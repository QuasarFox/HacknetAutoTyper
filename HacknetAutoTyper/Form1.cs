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
using System.Runtime.InteropServices;

namespace HacknetAutoTyper {
	public partial class Form1: Form {
		[DllImport("user32.dll")]
		public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
		[DllImport("user32.dll")]
		public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
		const int hotkeyID = 1;
		const int hotkeyID2 = 2;
		public Form1() {
			InitializeComponent();
			RegisterHotKey(this.Handle, hotkeyID, 0, (int) Keys.NumPad7);
			RegisterHotKey(this.Handle, hotkeyID2, 0, (int) Keys.NumPad4);
		}

		private void textBox1_TextChanged(object sender, EventArgs e) { }
		private void textBox3_TextChanged(object sender, EventArgs e) { }
		private void textBox2_TextChanged(object sender, EventArgs e) { }
		private void label1_Click(object sender, EventArgs e) { }
		private void textBox4_TextChanged(object sender, EventArgs e) {	}
		private void label4_Click(object sender, EventArgs e) {	}
		private void label5_Click(object sender, EventArgs e) {	}
		private void Form1_Load(object sender, EventArgs e) {
			textBox4.Text = @"C:\Program Files (x86)\Steam\userdata\101634916\365450\remote";
        }
		private void button3_Click(object sender, EventArgs e) {
			string Heart = "";
			int i = 0;
			while(Heart == "") {
				i++;
				try {
					String saveData = System.IO.File.ReadAllText(textBox4.Text + @"\save_" + textBox3.Text + ".xml");
					Heart = getHeartIPFromData(saveData, "computer name=\"Porthack.Heart \" ip=\"", "\" type=\"3\" spec=\"none\" id=\"porthackHeart");
					textBox1.Text = Heart;
				}
				catch(IOException) {
					if(i >= 2000) {
						MessageBox.Show("Error finding the savegame. \nCheck the name and path?");
						return;
					}
				}
			}
		}
		//Original code by Oscar Jara on Stackoverflow
		public static string getHeartIPFromData(string strSource, string strStart, string strEnd) {
			int Start, End;
			if(strSource.Contains(strStart) && strSource.Contains(strEnd)) {
				Start = strSource.IndexOf(strStart, 0) + strStart.Length;
				End = strSource.IndexOf(strEnd, Start);
				return strSource.Substring(Start, End - Start);
			}
			else {
				return "";
			}
		}
		protected override void WndProc(ref Message m) {
			if(m.Msg == 0x0312 && m.WParam.ToInt32() == hotkeyID) {
				//Hotkey #1
				SendKeys.Send("connect " + textBox1.Text);

				/*
				Note: 
				For some reason Enter doesn't work inside Hacknet.
				I tested it in multiple other programs and it worked everywhere EXCEPT Hacknet.
				Maybe Hacknet has some weird way of handling the Enter key? Idfk. 
				*/

				//SendKeys.Send("{ENTER}");

			}
			if(m.Msg == 0x0312 && m.WParam.ToInt32() == hotkeyID2) {
				//Hotkey #2
				SendKeys.Send("Porthack");
				//SendKeys.Send("{ENTER}");
			}
			base.WndProc(ref m);
		}
	}
}