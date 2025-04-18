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
using static System.Net.WebRequestMethods;

namespace Kondenzatory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Výchozí hodnoty pro výběr jednotek
            cmbUn.SelectedIndex = 4;
            cmbC.SelectedIndex = 6;
            cmbRn.SelectedIndex = 3;
            cmbRv.SelectedIndex = 3;
            cmbT1.SelectedIndex = 0;
            cmbT2.SelectedIndex = 0;
            cmbTk.SelectedIndex = 1;

            lblPolozky.Text = "Počet položek: " + lstViewData.Items.Count.ToString();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            // Získání hodnot
            double Un = (double)nmUn.Value;
            double C = (double)nmC.Value;
            double Rn = (double)nmRn.Value;
            double Rv = (double)nmRv.Value;
            double T1 = (double)nmT1.Value;
            double T2 = (double)nmT2.Value;
            double Tk = (double)nmTk.Value;

            // převod na základní jednotky
            // Un
            switch (cmbUn.SelectedIndex)
            {
                case 0: // tera
                    Un = Un * 1e12;
                    break;
                case 1: // giga
                    Un = Un * 1e9;
                    break;
                case 2: // mega
                    Un = Un * 1e6;
                    break;
                case 3: // kilo
                    Un = Un * 1e3;
                    break;
                case 4: // base
                    Un = Un;
                    break;
                case 5: // mili
                    Un = Un * 1e-3;
                    break;
                case 6: // mikro
                    Un = Un * 1e-6;
                    break;
                case 7: // nano
                    Un = Un * 1e-9;
                    break;
                case 8: // piko
                    Un = Un * 1e-12;
                    break;
            }
            // C
            switch (cmbC.SelectedIndex)
            {
                case 0: // tera
                    C = C * 1e12;
                    break;
                case 1: // giga
                    C = C * 1e9;
                    break;
                case 2: // mega
                    C = C * 1e6;
                    break;
                case 3: // kilo
                    C = C * 1e3;
                    break;
                case 4: // base
                    C = C;
                    break;
                case 5: // mili
                    C = C * 1e-3;
                    break;
                case 6: // mikro
                    C = C * 1e-6;
                    break;
                case 7: // nano
                    C = C * 1e-9;
                    break;
                case 8: // piko
                    C = C * 1e-12;
                    break;
            }
            // Rn
            switch (cmbRn.SelectedIndex)
            {
                case 0: // tera
                    Rn = Rn * 1e12;
                    break;
                case 1: // giga
                    Rn = Rn * 1e9;
                    break;
                case 2: // mega
                    Rn = Rn * 1e6;
                    break;
                case 3: // kilo
                    Rn = Rn * 1e3;
                    break;
                case 4: // base
                    Rn = Rn;
                    break;
                case 5: // mili
                    Rn = Rn * 1e-3;
                    break;
                case 6: // mikro
                    Rn = Rn * 1e-6;
                    break;
                case 7: // nano
                    Rn = Rn * 1e-9;
                    break;
                case 8: // piko
                    Rn = Rn * 1e-12;
                    break;
            }
            // Rv
            switch (cmbRv.SelectedIndex)
            {
                case 0: // tera
                    Rv = Rv * 1e12;
                    break;
                case 1: // giga
                    Rv = Rv * 1e9;
                    break;
                case 2: // mega
                    Rv = Rv * 1e6;
                    break;
                case 3: // kilo
                    Rv = Rv * 1e3;
                    break;
                case 4: // base
                    Rv = Rv;
                    break;
                case 5: // mili
                    Rv = Rv * 1e-3;
                    break;
                case 6: // mikro
                    Rv = Rv * 1e-6;
                    break;
                case 7: // nano
                    Rv = Rv * 1e-9;
                    break;
                case 8: // piko
                    Rv = Rv * 1e-12;
                    break;
            }
            // T1
            switch (cmbT1.SelectedIndex)
            {
                case 0: // base
                    T1 = T1;
                    break;
                case 1: // mili
                    T1 = T1 * 1e-3;
                    break;
                case 2: // mikro
                    T1 = T1 * 1e-6;
                    break;
                case 3: // nano
                    T1 = T1 * 1e-9;
                    break;
                case 4: // piko
                    T1 = T1 * 1e-12;
                    break;
            }
            // T2
            switch (cmbT2.SelectedIndex)
            {
                case 0: // base
                    T2 = T2;
                    break;
                case 1: // mili
                    T2 = T2 * 1e-3;
                    break;
                case 2: // mikro
                    T2 = T2 * 1e-6;
                    break;
                case 3: // nano
                    T2 = T2 * 1e-9;
                    break;
                case 4: // piko
                    T2 = T2 * 1e-12;
                    break;
            }
            // Tk
            switch (cmbTk.SelectedIndex)
            {
                case 0: // base
                    Tk = Tk;
                    break;
                case 1: // mili
                    Tk = Tk * 1e-3;
                    break;
                case 2: // mikro
                    Tk = Tk * 1e-6;
                    break;
                case 3: // nano
                    Tk = Tk * 1e-9;
                    break;
                case 4: // piko
                    Tk = Tk * 1e-12;
                    break;
            }

            double tauN = Rn * C;
            double tauV = Rv * C;
            double Imax = Un / Rn;

            lstViewData.Items.Clear();

            if (rdN.Checked)
            {
                // Výpočet pro nabíjení kondenzátoru
                for (double t = T1; t <= T2; t += Tk)
                {
                    double uc, ur, i;

                    uc = Un * (1 - Math.Exp(-t / tauN));
                    ur = Un * Math.Exp(-t / tauN);
                    i = Imax * Math.Exp(-t / tauN);

                    ListViewItem item = new ListViewItem(i.ToString("F6"));
                    item.SubItems.Add(uc.ToString("F6"));
                    item.SubItems.Add(ur.ToString("F6"));
                    item.SubItems.Add(i.ToString("F6"));
                    item.SubItems.Add(Un.ToString("F6"));
                    item.SubItems.Add(C.ToString("F6"));
                    item.SubItems.Add(Rn.ToString("F6"));
                    item.SubItems.Add(T1.ToString("F6"));
                    item.SubItems.Add(T2.ToString("F6"));
                    item.SubItems.Add(Tk.ToString("F6"));
                    lstViewData.Items.Add(item);

                    lblPolozky.Text = "Počet položek: " + lstViewData.Items.Count.ToString();
                }
            }
            else if (rdV.Checked)
            {
                // Výpočet pro vybíjení kondenzátoru
                for(double t = T1; t <= T2;t += Tk)
                {
                    double uc, ur, i;

                    uc = Un * Math.Exp(-t / tauV);
                    ur = -Un * Math.Exp(-t / tauV);
                    i = -Imax * Math.Exp(-t / tauV);

                    ListViewItem item = new ListViewItem(i.ToString("F6"));
                    item.SubItems.Add(uc.ToString("F6"));
                    item.SubItems.Add(ur.ToString("F6"));
                    item.SubItems.Add(i.ToString("F6"));
                    item.SubItems.Add(Un.ToString("F6"));
                    item.SubItems.Add(C.ToString("F6"));
                    item.SubItems.Add(Rn.ToString("F6"));
                    item.SubItems.Add(T1.ToString("F6"));
                    item.SubItems.Add(T2.ToString("F6"));
                    item.SubItems.Add(Tk.ToString("F6"));
                    lstViewData.Items.Add(item);

                    lblPolozky.Text = "Počet položek: " + lstViewData.Items.Count.ToString();
                }
            }
        }

        private void uložitToolStripButton_Click(object sender, EventArgs e)
        {
            dlgFileSave.InitialDirectory = Application.StartupPath;
            dlgFileSave.Filter = "CSV soubory (*.csv)|*.csv|Všechny soubory (*.*)|*.*";
            dlgFileSave.FileName = "Data.csv";

            DialogResult odpoved = dlgFileSave.ShowDialog();
            if (odpoved == DialogResult.OK)
            {
                string jmenoSouboru = dlgFileSave.FileName;

                using (StreamWriter soubor = new StreamWriter(jmenoSouboru, false,
                    Encoding.GetEncoding(1250)))
                {
                    // Zápis hlavičky
                    string hlavicka = "t;Uc;Ur;I;Un;C;R;T1;T2;Tk";
                    soubor.WriteLine(hlavicka);

                    // Zápis dat z ListView
                    foreach (ListViewItem item in lstViewData.Items)
                    {
                        StringBuilder radek = new StringBuilder();

                        // Projde všechny sloupce v řádku a přidá je do řetězce
                        for (int i = 0; i < item.SubItems.Count; i++)
                        {
                            radek.Append(item.SubItems[i].Text);

                            // Přidá středník, pokud není poslední položka
                            if (i < item.SubItems.Count - 1)
                            {
                                radek.Append(";");
                            }
                        }

                        soubor.WriteLine(radek.ToString());
                    }
                }
            }
        }

        private void novýToolStripButton_Click(object sender, EventArgs e)
        {
           lstViewData.Items.Clear();
            // Výchozí hodnoty pro výběr jednotek
            cmbUn.SelectedIndex = 4;
            cmbC.SelectedIndex = 6;
            cmbRn.SelectedIndex = 3;
            cmbRv.SelectedIndex = 3;
            cmbT1.SelectedIndex = 0;
            cmbT2.SelectedIndex = 0;
            cmbTk.SelectedIndex = 1;

            lblPolozky.Text = "Počet položek: " + lstViewData.Items.Count.ToString();
        }
    }
}
