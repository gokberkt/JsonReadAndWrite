using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonOkumaYazma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            ReadAndWriteJson();
        }

        public void ReadAndWriteJson()
        {

            Random rastgele = new Random();
            int sayi = rastgele.Next(0, 999999);
            StreamReader sr = new StreamReader(Application.StartupPath + "/human.json");
            List<Info> liste = JsonConvert.DeserializeObject<List<Info>>(sr.ReadToEnd());
            sr.Close();
            Info yeni = new Info();
            yeni.Name = txtName.Text;
            yeni.LastName = txtLastName.Text;
            yeni.ID = sayi;
            liste.Add(yeni);
            string lastRaw = JsonConvert.SerializeObject(liste);
            System.IO.File.WriteAllText(Application.StartupPath + "/human.json", lastRaw);
            dgvJson.DataSource = liste;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(Application.StartupPath + "/human.json");
            List<Info> liste = JsonConvert.DeserializeObject<List<Info>>(sr.ReadToEnd());
            sr.Close();

            lblID.Text = dgvJson.SelectedRows[0].Cells[0].Value.ToString();
            var silinecek = liste.Find(x => x.ID == Convert.ToInt32(lblID.Text));
            liste.Remove(silinecek);

            string lastRaw = JsonConvert.SerializeObject(liste);
            System.IO.File.WriteAllText(Application.StartupPath + "/human.json", lastRaw);
            dgvJson.DataSource = liste;

        }
    }
}
