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
        }

        void Atualizar(int indice)
        {
            tela[indice].Text = roleta[indice].ToString();
        }

        private void btGirar_Click(object sender, EventArgs e)
        {
            for(int i = 0; i< roleta.Length; i++)
            {
                tempos[i] = r.Next(1, 21);
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
                /*for (int i = 0;i < roleta.Length; i++)
                {
                    roleta[i] = 7;
                }*/

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
    }
}
