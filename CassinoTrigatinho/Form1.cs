using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CassinoTrigatinho
{
    public partial class Form1 : Form
    {
        int[] tempos;
        int[] roleta;
        Random r;
        Label[] tela;
        public Form1()
        {
            InitializeComponent();
            roleta = new int[3];
            tempos = new int[3];
            tela = new Label[] { lbl1, lbl2, lbl3 };
            r = new Random();
            for(int i = 0; i < roleta.Length; i++)
            {
                roleta[i] = r.Next(0, 10);
                Atualizar(i);
            }
            for (int j = 0; j < roleta.Length; j++)
            {
                tela[j].Text = 0.ToString();
            }
        }

        void Atualizar(int indice)
        {
            tela[indice].Text = roleta[indice].ToString();
        }

        private void btGirar_Click(object sender, EventArgs e)
        {
            for(int i = 0; i< roleta.Length; i++)
            {
                tempos[i] = r.Next(0,21);
                tela[i].ForeColor = Color.Black;
            }
            Array.Sort(tempos);
            btGirar.Enabled = false;
            tmrGiro.Enabled = true;
        }

        private void tmrGiro_Tick(object sender, EventArgs e)
        {
            bool parado = true;
            for (int i = 0; i < roleta.Length; i++)
            {
                if (tempos[i] > 0)
                {
                    tempos[i]--;
                    if (tempos[i] == 0)
                    {
                        tela[i].ForeColor = Color.Black;
                    }
                    roleta[i]++;
                    if (roleta[i] == 10)
                    {
                        roleta[i] = 0;
                    }
                    Atualizar(i);
                    parado &= false;
                }

            }
            if (parado)
            {
                btGirar.Enabled = true;
                tmrGiro.Enabled = false;
                lbxUltimos.Items.Add($"{roleta[0]}-{roleta[1]}-{roleta[2]}");
                //rtbUltimos.Text = $"{roleta[0]}-{roleta[1]}-{roleta[2]}\n"+rtbUltimos.Text;
                if (roleta[0] == roleta[1] && roleta[1] == roleta[2])
                {
                    MessageBox.Show("PARABÉNS! VOCÊ GANHOU! 🎉🎊", "VITÓRIA!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    foreach (Label lbl in tela)
                    {
                        lbl.ForeColor = Color.Green;
                    }
                }
            }
        }
        List<string> jogadas;
        private void chbVitorias_CheckedChanged(object sender, EventArgs e)
        {
            if (chbVitorias.Checked)
            {
                jogadas = new List<string>();
                foreach (string item in lbxUltimos.Items)
                {
                    jogadas.Add(item);
                }
                lbxUltimos.Items.Clear();
                foreach (string item in jogadas)
                {
                    string[] nums = item.Split('-');
                    if (nums[0] == nums[1] && nums[1] == nums[2])
                    {
                        lbxUltimos.Items.Add(item);
                    }
                }
            }
            else
            {
                lbxUltimos.Items.Clear();
                foreach (string item in jogadas)
                {
                    lbxUltimos.Items.Add(item);
                }
            }
        }
    }
}
