using ExcelDataReader;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;


/* 一张xlsx里面有多张表格，因为实际游戏中使用的是json文件，所以生成的时候我只用一张xlsx
  * 一张sheet就是一个数据集合 
  * 根据sheet表字段生成对应的数据集合json
  * 可以创建实体类
  * 表格第一行为列名
  * 表格第二行为类型
  * 数组用,分割
  */
public class ExcelWindow : EditorWindow
{
    private string txt_EntitySavePath = "Entity";

    private Vector2 scrollPos;

    private string exclePath = "xxx";
    private string jsonSavePath = "xxx";

    [MenuItem ("Window/ExcelToJson")]
    static void OpenWindow ()
    {
        ExcelWindow window = (ExcelWindow)EditorWindow.GetWindow (typeof (ExcelWindow));
        window.Show ();
    }

    private void OnGUI ()
    {
        scrollPos = EditorGUILayout.BeginScrollView (scrollPos);

        exclePath = string.Concat (Application.dataPath, "/i3.xlsx");
        jsonSavePath = Application.streamingAssetsPath;

        EditorGUILayout.BeginVertical ();
        GUILayout.Label ("表格路径: ");
        GUILayout.Space (5);
        exclePath = GUILayout.TextField (exclePath);
        EditorGUILayout.EndVertical ();

        GUILayout.Space (5);

        EditorGUILayout.BeginVertical ();
        GUILayout.Label ("Json保存路径: ");
        GUILayout.Space (5);
        jsonSavePath = GUILayout.TextField (jsonSavePath);
        EditorGUILayout.EndVertical ();

        GUILayout.Space (5);

        if (GUILayout.Button ("选择文件"))
        {
            SelectExcleFile ();           
        }

        GUILayout.Space (5);

        if (GUILayout.Button ("选择Json保存目录"))
        {
            SelectJsonSaveDirectory ();
        }
        GUILayout.Space (5);

        if (GUILayout.Button ("生成实体类"))
        {
            CreateEntities ();
        }
        GUILayout.Space (5);

        if (GUILayout.Button ("生成Json"))
        {
            //ExcelToJson ();
            ConvertToJson ();
        }
        EditorGUILayout.EndScrollView ();
    }


    private void SelectExcleFile ()
    {
        //string path = EditorUtility.OpenFilePanel ("Overwrite with png" , Application.dataPath , "xlsx");
        string path = EditorUtility.OpenFilePanelWithFilters ("Select Excel" , Application.dataPath , new string[] { "*", "xlsx,xls"});
        if (path.Length != 0)
        {
            exclePath = path;
        }
    }

    private void SelectJsonSaveDirectory ()
    {
        string path = EditorUtility.OpenFolderPanel ("Select Folder" , Application.dataPath, "");
        if (path.Length != 0)
        {
            jsonSavePath = path;
        }
    }

    private void ConvertToJson ()
    {
        if (!string.IsNullOrEmpty (exclePath))
        {
            using (FileStream fs = new FileStream (exclePath , FileMode.Open , FileAccess.Read))
            {
                using (ExcelPackage ep = new ExcelPackage (fs))
                {
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader (fs);

                    DataSet result = excelReader.AsDataSet ();

                    // 判断Excel文件中是否存在数据表
                    if (result.Tables.Count < 1)
                        return;

                    // 准备一个列表存储整个表的数据
                    List<Dictionary<string , object>> table = new List<Dictionary<string , object>> ();

                    // 遍历所有工作表
                    for (int i = 0; i < result.Tables.Count; i++)
                    {
                        table.Clear ();

                        int columnCount = result.Tables[i].Columns.Count;
                        int rowCount = result.Tables[i].Rows.Count;

                        // 读取数据
                        // 第一行列名
                        // 第二行类型
                        // 所以从第三行开始
                        for (int j = 2; j < rowCount; j++)
                        {
                            // 准备一个字典存储每一行的数据
                            Dictionary<string , object> row = new Dictionary<string , object> ();
                            for (int k = 0; k < columnCount; k++)
                            {
                                // 读取第1行数据作为表头字段
                                string field = result.Tables[i].Rows[0][k].ToString ();
                                // 读取第二行并且转换为对应Type
                                Type type = GetTypeByString (result.Tables[i].Rows[1][k].ToString ());

                                if (type != null)
                                {
                                    // 将单元格里的数据转换为对应格式的内容
                                    object value = Convert.ChangeType (result.Tables[i].Rows[j][k].ToString () , type);
                                    //Key-Value对应
                                    row[field] = value;
                                }
                                else
                                {
                                    object value = CovertToArry (result.Tables[i].Rows[1][k].ToString () , result.Tables[i].Rows[j][k].ToString ());
                                    //Key-Value对应
                                    row[field] = value;
                                }
                            }

                            //添加到表数据中
                            table.Add (row);
                        }

                        //写入json文件
                        string jsonPath = $"{jsonSavePath}/{result.Tables[i].TableName}.json";

                        if (!File.Exists (jsonPath))
                        {
                            File.Create (jsonPath).Dispose ();
                        }
                        File.WriteAllText (jsonPath , JsonFx.Json.JsonWriter.Serialize (table));

                        Debug.LogError ($"Convert Excel To Json Success Path==>{jsonPath}");
                    }
                }
            }
            AssetDatabase.Refresh ();
        }
    }
    
    private void ExcelToJson ()
    {
        if (!string.IsNullOrEmpty (exclePath))
        {
            using (FileStream fs = new FileStream (exclePath , FileMode.Open , FileAccess.Read))
            {
                using (ExcelPackage ep = new ExcelPackage (fs))
                {
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader (fs);

                    DataSet result = excelReader.AsDataSet ();


                    List<System.Object> lst = new List<object> ();

                    //遍历所有工作表
                    for (int i = 0; i < result.Tables.Count; i++)
                    {
                        lst.Clear ();

                        int columnCount = result.Tables[i].Columns.Count;
                        int rowCount = result.Tables[i].Rows.Count;

                        //根据实体类创建对象集合序列化到json中
                        for (int z = 2; z < rowCount; z++)
                        {
                            Assembly ab = Assembly.Load ("Assembly-CSharp"); //要注意对面在那个程序集里面dll
                            Type type = ab.GetType ($"Entity.{result.Tables[i].TableName}");
                            if (type == null)
                            {
                                Debug.LogError ("你还没有创建对应的实体类!");
                                return;
                            }
                            if (!Directory.Exists (jsonSavePath))
                                Directory.CreateDirectory (jsonSavePath);
                            object o = ab.CreateInstance (type.ToString ());
                            for (int j = 0; j < columnCount; j++)
                            {
                                Debug.LogError (result.Tables[i].Rows[z][j].ToString ());
                                FieldInfo fieldInfo = type.GetField (result.Tables[i].Rows[0][j].ToString ()); //先获得字段信息，方便获得字段类型
                                try
                                {
                                    Debug.LogError (fieldInfo.FieldType);
                                    Debug.LogError (fieldInfo.FieldType.ToString ().Equals ("System.Int32[]"));
                                    object value = Convert.ChangeType (result.Tables[i].Rows[z][j].ToString () , fieldInfo.FieldType);
                                    //Debug.LogError (result.Tables[i].Rows[1][j].ToString ());
                                    //Debug.LogError (o);

                                    type.GetField (result.Tables[i].Rows[0][j].ToString ()).SetValue (o , value);
                                }
                                catch (InvalidCastException e)
                                {
                                    object value = CovertToArry (fieldInfo.FieldType.ToString () , result.Tables[i].Rows[z][j].ToString ());
                                    type.GetField (result.Tables[i].Rows[0][j].ToString ()).SetValue (o , value);
                                    //Debug.LogError (e);
                                }

                            }
                            lst.Add (o);
                        }
                        //写入json文件
                        string jsonPath = $"{jsonSavePath}/{result.Tables[i].TableName}.json";

                        if (!File.Exists (jsonPath))
                        {
                            File.Create (jsonPath).Dispose ();
                        }
                        File.WriteAllText (jsonPath , JsonFx.Json.JsonWriter.Serialize (lst));

                        Debug.LogError ($"Excel To Json Success Path==>{jsonPath}");
                    }
                }
            }
            AssetDatabase.Refresh ();
        }
    }

    void CreateEntities ()
    {
        if (!string.IsNullOrEmpty (exclePath))
        {
            using (FileStream fs = new FileStream (exclePath , FileMode.Open , FileAccess.Read))
            {
                using (ExcelPackage ep = new ExcelPackage (fs))
                {
                    //获得所有工作表
                    ExcelWorksheets workSheets = ep.Workbook.Worksheets;
                    //遍历所有工作表
                    for (int i = 0; i < workSheets.Count; i++)
                    {
                        CreateEntity (workSheets[i]);
                    }
                    AssetDatabase.Refresh ();
                }
            }
        }
    }

    void CreateEntity (ExcelWorksheet sheet)
    {
        string dir = $"{Application.dataPath}/{txt_EntitySavePath}";
        string path = $"{dir}/{sheet.Name}.cs";
        StringBuilder sb = new StringBuilder ();
        sb.AppendLine ("namespace Entity");
        sb.AppendLine ("{");
        sb.AppendLine ($"\tpublic class {sheet.Name}");
        sb.AppendLine ("\t{");
        //遍历sheet首行每个字段描述的值
        Debug.Log ("column = " + sheet.Dimension.End.Column);
        Debug.Log ("row = " + sheet.Dimension.End.Row);
        for (int i = 1; i <= sheet.Dimension.End.Column; i++)
        {
            sb.AppendLine ("\t\t/// <summary>");
            sb.AppendLine ($"\t\t///{sheet.Cells[3 , i].Text}");
            sb.AppendLine ("\t\t/// </summary>");
            sb.AppendLine ($"\t\tpublic {sheet.Cells[2 , i].Text} {sheet.Cells[1 , i].Text};");
        }
        sb.AppendLine ("\t}");
        sb.AppendLine ("}");
        try
        {
            if (!Directory.Exists (dir))
            {
                Directory.CreateDirectory (dir);
            }
            if (!File.Exists (path))
            {
                File.Create (path).Dispose (); //避免资源占用
            }
            File.WriteAllText (path , sb.ToString ());
        }
        catch (System.Exception e)
        {
            Debug.LogError ($"Excel转json时创建对应的实体类出错，实体类为：{sheet.Name},e:{e.Message}");
        }
    }

    Type GetTypeByString (string typeName)
    {
        Type result = null;
        string realTypeName = typeName;
        switch (typeName.Trim ().ToLower ())
        {
            case "bool":
                realTypeName = "System.Boolean";
                break;
            case "byte":
                realTypeName = "System.Byte";
                break;
            case "sbyte":
                realTypeName = "System.SByte";
                break;
            case "char":
                realTypeName = "System.Char";
                break;
            case "decimal":
                realTypeName = "System.Decimal";
                break;
            case "double":
                realTypeName = "System.Double";
                break;
            case "float":
                realTypeName = "System.Single";
                break;
            case "int":
                realTypeName = "System.Int32";
                break;
            case "uint":
                realTypeName = "System.UInt32";
                break;
            case "long":
                realTypeName = "System.Int64";
                break;
            case "ulong":
                realTypeName = "System.UInt64";
                break;
            case "object":
                realTypeName = "System.Object";
                break;
            case "short":
                realTypeName = "System.Int16";
                break;
            case "ushort":
                realTypeName = "System.UInt16";
                break;
            case "string":
                realTypeName = "System.String";
                break;
            case "date":
            case "datetime":
                realTypeName = "System.DateTime";
                break;
            case "guid":
                realTypeName = "System.Guid";
                break;
            default:
                break;
        }

        try
        {
            result = Type.GetType (realTypeName , true , true);
        }
        catch (Exception e) {
            Debug.LogWarning ($"Get  {typeName}  Error {e}");
        }

        return result;
    }

    object CovertToArry (string type, string value)
    {        
        string[] strArry = value.Split (',');
        int lgh = strArry.Length;

        if (type.ToLower ().Contains ("int"))
        {
            int[] result = new int[lgh];
            for (int i = 0; i < lgh; i++)
            {
                if (string.IsNullOrEmpty (strArry[i]))
                {
                    result[i] = 0;
                }
                else
                {
                    result[i] = int.Parse (strArry[i]);
                }                
            }
            return result;
        }
        else if (type.ToLower ().Contains ("string"))
        {
            return strArry;
        }
        return null;
    }
}