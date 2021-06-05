using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Common;
using System.Windows.Forms;

namespace PPR
{
    class Equipment
        {
            public int indexNameEquip;
            public int indexEqip;
            public string Typeofrepair;
            public DateTime dateofrepair;
        }
    class Connection
    {

       // private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=D:\ПРОЕКТЫ VS\PPR\PPR.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //private string connectionString = Application.StartupPath + "\\PPR\\PPR.mdf";
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\PPR.mdf';Integrated Security=True";
        //private string connectionString = @"Data Source=" + Application.StartupPath + "\\PPR.mdf;Persist Security Info=False"; 
        public ArrayList GetAllEquipment()//Вывод списка всего оборудования
        {
            ArrayList arr = new ArrayList();
            string command = String.Format("Select [Name_equipment].[Id] As 'Инвент №', [Equipment].[RepairID], [Equipment].[Name] As 'Оборудование', [Name_equipment].[Name] As 'Наименование', [Name_equipment].[Model] As 'Модель', [Shop].[Name] As 'Цех', [Name_equipment].[CommissioningYear], [Name_equipment].[YearOfIssue],[Name_equipment].[CathegoryOfRepair] As 'Категория ремонта', [Name_equipment].[TypeOfLastRepair], [Name_equipment].[DateOfLastRepair], [Name_equipment].[1], [Name_equipment].[2], [Name_equipment].[3], [Name_equipment].[4], [Name_equipment].[5], [Name_equipment].[6], [Name_equipment].[7], [Name_equipment].[8], [Name_equipment].[9], [Name_equipment].[10], [Name_equipment].[11], [Name_equipment].[12], [Name_equipment].[TypeOfRepair], [Name_equipment].[DateOfRepair] From [Name_equipment], [Equipment], [Shop] Where [Name_equipment].[EquipmentID] = [Equipment].[Id] and [Name_equipment].[ShopID] = [Shop].[Id] Order by [Shop].[Name]");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(command, con);
                try
                {
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                        foreach (DbDataRecord result in dr)
                            arr.Add(result);

                    con.Close();

                }
                catch { }
                           
                            return arr;
            }
            
        }
       /* public Equipment GetEquipment(int index)
        {
            Equipment equipment = new Equipment();
            equipment.indexNameEquip = index;
            string command = string.Format("Select [Name_equipment].[EquipmentID], [Name_equipment].[TypeOfLastRepair], [Name_equipment].[DateOfLastRepair] From [Name_equipment] Where [Name_equipment].[Id] = '{0}'", index);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(command, con);
                try
                {
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();
                         while (dr.Read())
                        { 
                            equipment.indexEqip = Convert.ToInt32(dr["EquipmentID"].ToString());
                            equipment.Typeofrepair = dr["TypeOfLastRepair"].ToString();
                            equipment.dateofrepair = Convert.ToDateTime(dr["DateOfLastRepair"].ToString());
                        }
                    con.Close();
                }
                catch { }
            }
                return equipment;
               
         }*/
        public ArrayList DoCount(Equipment equipment, int year)
        {
            int yearRep = year;
            
            ArrayList arr = new ArrayList();
            string command = String.Format("Select * From Repair Where Id = '{0}'", equipment.indexEqip);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(command, con);
                con.Open();
                 try
                {
                     SqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        int j = Convert.ToInt32(dr[dr.FieldCount-1].ToString());
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            
                            if (dr[i].ToString() == equipment.Typeofrepair.ToString())
                            {
                                if (dr[i + 1].ToString() != "")
                                {
                                    equipment.Typeofrepair = dr[i + 1].ToString();
                                    //date = equipment.dateofrepair;
                                    //equipment.dateofrepair = date.AddMonths(j);
                                    if (!Count(equipment, j, yearRep))
                                    { break; }
                                    i = i - 1;
                                }
                                else
                                {
                                    i = 0;
                                    equipment.Typeofrepair = dr[i + 1].ToString();
                                    if (!Count(equipment, j, yearRep))
                                    { break; }
                                    i = i - 1;
                                }
                               
                            }
                        }
                     }
                     con.Close();
                }
                catch (Exception e)
                {
                    e.ToString();
                }

            }
            return arr;
        }
        private bool Count(Equipment equipment, int period, int yearRep)
        {

            bool flagresult = false;
            equipment.dateofrepair = equipment.dateofrepair.AddMonths(period);
            int year = yearRep;
            if (equipment.dateofrepair.Month == 1)
            {
                equipment.dateofrepair = equipment.dateofrepair.AddMonths(1);
            }
            if (year == equipment.dateofrepair.Year)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //SqlCommand com = new SqlCommand(command, con);
                    con.Open();
                    SqlTransaction sqlTransact = con.BeginTransaction();
                    SqlCommand com = con.CreateCommand();
                    com.Transaction = sqlTransact;
                    try

                    {
                        string command = string.Format("Update Name_equipment Set [Name_equipment].[" + equipment.dateofrepair.Month + "] = ('{0}'), [Name_equipment].[DateOfRepair] = ('{1}'), [Name_equipment].[TypeOfRepair] = ('{0}') Where [Name_equipment].[Id] = '{2}' ", equipment.Typeofrepair, equipment.dateofrepair.ToString("yyyy-MM-dd"), equipment.indexNameEquip);
                        com.CommandText = command;
                        com.ExecuteNonQuery();
                        sqlTransact.Commit();
                        con.Close();

                    }
                    catch (Exception e) { e.ToString(); }
                }
                flagresult = true;
            }
            return flagresult;
        }

        public ArrayList GetAllShops()// Вывод списка всех подразделений
        {
            ArrayList arr = new ArrayList();
            string command = String.Format("Select [Id] As 'Порядковый № подразделения', [Name] As 'Наименование' From Shop");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(command, con);
                try
                {
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                        foreach (DbDataRecord result in dr)
                            arr.Add(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(Convert.ToString(e), "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
         return arr;
        }
        public bool InsertShop(string Name)//Добавление подразделения
        {
            string name = Name;
            bool flagresult = false;
            string command = string.Format("Insert into Shop (Name) values (N'{0}')", Convert.ToString(Name));
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(command, con);
                try
                {
                    con.Open();
                    com.ExecuteNonQuery();
                    flagresult = true;
                }
                catch { flagresult = false; }
                con.Close();
            }
                return flagresult;
        }
        public bool UpdateShop(int index, string Name)
        {
            bool flagresult = false;
            string command = string.Format("Update Shop Set [Shop].[Name] = (N'{0}') Where [Shop].[Id] = '{1}'", Name, index);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(command, con);
                try
                {
                    con.Open();
                    com.ExecuteNonQuery();
                    flagresult = true;
                }
                catch { flagresult = false; }
                con.Close();
            }
            return flagresult;
        }
        public bool DeleteShop(int index)
        {
            bool flagresult = false;
            string command = string.Format("Delete From Shop Where [Shop].[Id] = '{0}'", index);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(command, con);
                try
                {
                    con.Open();
                    com.ExecuteNonQuery();
                    flagresult = true;
                }
                catch { flagresult = false; }
                con.Close();
            }
            return flagresult;
        }
        public ArrayList Search(int index)
        {
            ArrayList arr = new ArrayList();
            string command = string.Format("Select [Name_equipment].[Id] As 'Инвент №', [Equipment].[RepairID], [Equipment].[Name] As 'Оборудование', [Name_equipment].[Name] As 'Наименование', [Shop].[Name] As 'Цех', [Name_equipment].[CommissioningYear], [Name_equipment].[YearOfIssue], [Name_equipment].[TypeOfLastRepair], [Name_equipment].[DateOfLastRepair], [Name_equipment].[1], [Name_equipment].[2], [Name_equipment].[3], [Name_equipment].[4], [Name_equipment].[5], [Name_equipment].[6], [Name_equipment].[7], [Name_equipment].[8], [Name_equipment].[9], [Name_equipment].[10], [Name_equipment].[11], [Name_equipment].[12], [Name_equipment].[TypeOfRepair], [Name_equipment].[DateOfRepair] From [Name_equipment], [Equipment], [Shop] Where [Name_equipment].[EquipmentID] = [Equipment].[Id] and [Name_equipment].[ShopID] = [Shop].[Id] and [Name_equipment].[Id] = '{0}'", index);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(command, con);
                try
                {
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                        foreach (DbDataRecord result in dr)
                            arr.Add(result);
                    else
                        MessageBox.Show("Не удалось найти искомое оборудование!", "ИНФОРМАЦИЯ!", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
                catch { }
                con.Close();
             }
            return arr;
        }
        public ArrayList GetEquipmentByMonth(int month, int num)
        {
            ArrayList arr = new ArrayList();
            string command = string.Format("select [Equipment].[Name] As 'Оборудование', [Name_equipment].[Name] As 'Наименование', [Name_equipment].[Model] As 'Модель', [Name_equipment].[Id] As 'Инвентарный №', [Name_equipment].[CathegoryOfRepair] As 'Категория ремонта', [Name_equipment].[" + month+ "] As 'Вид ремонта' From [Equipment],[Name_equipment] Where [Name_equipment].[EquipmentID] = [Equipment].[Id] and [Name_equipment].[ShopID] = (N'{0}')", num);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(command, con);
                try
                {
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        foreach(DbDataRecord record in dr)
                             { 
                                if (record[5].ToString() != "")
                                    arr.Add(record);
                             }
                    }
                    con.Close();
                }
                catch { }
            }
                return arr;
        }
        
    }

}
