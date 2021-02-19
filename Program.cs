using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace ShutdownGuiTool
{

	class MainForm : Form
	{
		/// <summary>
		/// size and location of the controls
		/// </summary>
		private const int BLANK_L = 7;
		private const int BLANK_S = 1;
		private const int WIDE_L = 40;
		private const int HIGH_S = 20;
		private const int HIGH_L = 23;
		private const int WIDE_S = 30;

		private const int FORMWIDE = BLANK_L * 2 + BLANK_S * 5 + WIDE_L * 3 + WIDE_S * 3 + 15;
		private const int FORMHIGH = BLANK_L * 3 + BLANK_S * 3 + HIGH_S * 3 + HIGH_L * 2 + 40;
		/// <summary>
		/// time
		/// </summary>
		public int delTime;

		/// <summary>
		/// controls
		/// </summary>
		/// 
		private TextBox textboxHour = new TextBox()
		{
			Location = new Point(BLANK_L, BLANK_L + BLANK_S + HIGH_S),
			Size = new Size(WIDE_L, HIGH_L),
			Multiline = true,
			MaxLength = 3
		};
		private TextBox textboxMinute = new TextBox()
		{
			Location = new Point(BLANK_L + (BLANK_S * 2 + WIDE_L + WIDE_S), BLANK_L + BLANK_S + HIGH_S),
			Size = new Size(WIDE_L, HIGH_L),
			Multiline = true,
			MaxLength = 3
		};
		private TextBox textboxSecond = new TextBox()
		{
			Location = new Point(BLANK_L + (BLANK_S * 2 + WIDE_L + WIDE_S) * 2, BLANK_L + BLANK_S + HIGH_S),
			Size = new Size(WIDE_L, HIGH_L),
			Multiline = true,
			MaxLength = 3
		};
		private Button buttonTimePlus_3600 = new Button()
		{
			Location = new Point(BLANK_L, BLANK_L),
			Size = new Size(WIDE_L, HIGH_S),
			Text = "+1h"
		};
		private Button buttonTimePlus_300 = new Button()
		{
			Location = new Point(BLANK_L + (BLANK_S * 2 + WIDE_L + WIDE_S), BLANK_L),
			Size = new Size(WIDE_L, HIGH_S),
			Text = "+5m"
		};
		private Button buttonTimePlus_30 = new Button()
		{
			Location = new Point(BLANK_L + (BLANK_S * 2 + WIDE_L + WIDE_S) * 2, BLANK_L),
			Size = new Size(WIDE_L, HIGH_S),
			Text = "+30s"
		};
		private Button buttonTimeMinus_3600 = new Button()
		{
			Location = new Point(BLANK_L, BLANK_L + HIGH_S + HIGH_L + BLANK_S * 2),
			Size = new Size(WIDE_L, HIGH_S),
			Text = "-1h"
		};
		private Button buttonTimeMinus_300 = new Button()
		{
			Location = new Point(BLANK_L + (BLANK_S * 2 + WIDE_L + WIDE_S), BLANK_L + HIGH_S + HIGH_L + BLANK_S * 2),
			Size = new Size(WIDE_L, HIGH_S),
			Text = "-5m"
		};
		private Button buttonTimeMinus_30 = new Button()
		{
			Location = new Point(BLANK_L + (BLANK_S * 2 + WIDE_L + WIDE_S) * 2, BLANK_L + HIGH_S + HIGH_L + BLANK_S * 2),
			Size = new Size(WIDE_L, HIGH_S),
			Text = "-30s"
		};
		private Button buttonResetTime = new Button()
		{
			Location = new Point(BLANK_L, BLANK_L * 2 + BLANK_S * 3 + HIGH_S * 2 + HIGH_L * 2),
			Size = new Size(WIDE_L, HIGH_L),
			Text = "重置"
		};

		private Button buttonShutdown = new Button()
		{
			Location = new Point(BLANK_L + BLANK_S * 5 + WIDE_L * 2 + WIDE_S * 3, BLANK_L * 2 + BLANK_S * 3 + HIGH_S * 2 + HIGH_L * 2),
			Size = new Size(WIDE_L, HIGH_L),
			Text = "关机"
		};
		private Button buttonReboot = new Button()
		{
			Location = new Point(BLANK_S * 5 + WIDE_L + WIDE_S * 3, BLANK_L * 2 + BLANK_S * 3 + HIGH_S * 2 + HIGH_L * 2),
			Size = new Size(WIDE_L, HIGH_L),
			Text = "重启"
		};
		private Button buttonCancel = new Button()
		{
			Location = new Point(BLANK_S * 5 + WIDE_L + WIDE_S * 3 - BLANK_L - WIDE_L, BLANK_L * 2 + BLANK_S * 3 + HIGH_S * 2 + HIGH_L * 2),
			Size = new Size(WIDE_L, HIGH_L),
			Text = "取消"
		};
		private Label labelTime = new Label()
		{
			Location = new Point(BLANK_L, BLANK_L * 2 + BLANK_S * 2 + HIGH_S * 2 + HIGH_L),
			Size = new Size(BLANK_S * 5 + WIDE_L * 3 + WIDE_S * 3, HIGH_L),
			Text=""
		};
		private void setControls()
		{
			KeyPressEventHandler onlyNumber = new KeyPressEventHandler((s, e) =>
			{
				if ((e.KeyChar <= '9' && e.KeyChar >= '0') || (e.KeyChar == (char)Keys.Back))
					e.Handled = false;
				else
				{
					e.Handled = true;
					if (e.KeyChar == (char)Keys.Enter)
						refresh0();
				}
			});
			textboxHour.KeyPress += onlyNumber;
			textboxMinute.KeyPress += onlyNumber;
			textboxSecond.KeyPress += onlyNumber;
			textboxHour.LostFocus += (s, e) => refresh0();
			textboxMinute.LostFocus += (s, e) => refresh0();
			textboxSecond.LostFocus += (s, e) => refresh0();
			buttonTimePlus_3600.Click += new EventHandler((s, e) =>
			{
				refresh0();
				delTime += 3600;
				refresh1();
			});
			buttonTimePlus_300.Click += new EventHandler((s, e) =>
			{
				refresh0();
				delTime += 300;
				refresh1();
			});
			buttonTimePlus_30.Click += new EventHandler((s, e) =>
			{
				refresh0();
				delTime += 30;
				refresh1();
			});
			buttonTimeMinus_3600.Click += new EventHandler((s, e) =>
			{
				refresh0();
				delTime -= 3600;
				refresh1();
			});
			buttonTimeMinus_300.Click += new EventHandler((s, e) =>
			{
				refresh0();
				delTime -= 300;
				refresh1();
			});
			buttonTimeMinus_30.Click += new EventHandler((s, e) =>
			{
				refresh0();
				delTime -= 30;
				refresh1();
			});
			buttonResetTime.Click += new EventHandler((s, e) => { reset(); });
			buttonShutdown.Click += new EventHandler((s, e) =>
			{
				var pSI = new ProcessStartInfo("shutdown.exe", @"/s /t " + delTime.ToString()) { CreateNoWindow = true };
				var p = new Process() { StartInfo = pSI };
				p.Start();
			});
			buttonCancel.Click += new EventHandler((s, e) =>
			{
				var pSI = new ProcessStartInfo("shutdown.exe", @"/a") { CreateNoWindow = true };
				var p = new Process() { StartInfo = pSI };
				p.Start();
			});
			buttonReboot.Click += new EventHandler((s, e) =>
			{
				var pSI = new ProcessStartInfo("shutdown.exe", @"/r /t " + delTime.ToString()) { CreateNoWindow = true };
				var p = new Process() { StartInfo = pSI };
				p.Start();
			});
		}
		/// <summary>
		/// 
		/// </summary>
		private void reset()
		{
			textboxHour.Text = "0";
			textboxMinute.Text = "5";
			textboxSecond.Text = "0";
			refresh0();
		}
		private void refresh0()
		{

			int h, m, s;
			try { h = Convert.ToInt32(textboxHour.Text); } catch { h = 0; }
			try { m = Convert.ToInt32(textboxMinute.Text); } catch { m = 0; }
			try { s = Convert.ToInt32(textboxSecond.Text); } catch { s = 0; }
			delTime = h * 3600 + m * 60 + s;
			if (delTime < 0)
				delTime = 0;
			h = delTime / 3600;
			m = delTime / 60 % 60;
			s = delTime % 60;
			textboxHour.Text = h.ToString();
			textboxMinute.Text = m.ToString();
			textboxSecond.Text = s.ToString();
			labelTime.Text = DateTime.Now.AddSeconds(delTime).ToLongDateString() + " " + DateTime.Now.AddSeconds(delTime).ToLongTimeString();
			return;
		}
		private void refresh1()
		{

			int h, m, s;
			if (delTime < 0)
				delTime = 0;
			h = delTime / 3600;
			m = delTime / 60 % 60;
			s = delTime % 60;
			textboxHour.Text = h.ToString();
			textboxMinute.Text = m.ToString();
			textboxSecond.Text = s.ToString();
			labelTime.Text = DateTime.Now.AddSeconds(delTime).ToLongDateString() + " " + DateTime.Now.AddSeconds(delTime).ToLongTimeString();
			return;
		}

		public MainForm()
		{
			Text = "定时关机";
			Size = new Size(FORMWIDE, FORMHIGH);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			AutoScaleMode = AutoScaleMode.None;
			setControls();

			Controls.Add(textboxHour);
			Controls.Add(textboxMinute);
			Controls.Add(textboxSecond);
			Controls.Add(buttonTimePlus_3600);
			Controls.Add(buttonTimeMinus_3600);
			Controls.Add(buttonTimePlus_300);
			Controls.Add(buttonTimeMinus_300);
			Controls.Add(buttonTimePlus_30);
			Controls.Add(buttonTimeMinus_30);
			Controls.Add(buttonResetTime);
			Controls.Add(buttonShutdown);
			Controls.Add(buttonReboot);
			Controls.Add(buttonCancel);
			Controls.Add(labelTime);

			var labelHour = new Label()
			{
				Location = new Point(BLANK_L + BLANK_S + WIDE_L, BLANK_L + BLANK_S + HIGH_S),
				Size = new Size(WIDE_L, HIGH_L),
				Text = "h"
			};
			var labelMinute = new Label()
			{
				Location = new Point(BLANK_L + BLANK_S + WIDE_L + (BLANK_S * 2 + WIDE_L + WIDE_S), BLANK_L + BLANK_S + HIGH_S),
				Size = new Size(WIDE_L, HIGH_L),
				Text = "m"
			};
			var labelSecond = new Label()
			{
				Location = new Point(BLANK_L + BLANK_S + WIDE_L + (BLANK_S * 2 + WIDE_L + WIDE_S) * 2, BLANK_L + BLANK_S + HIGH_S),
				Size = new Size(WIDE_L, HIGH_L),
				Text = "s"
			};
			Controls.Add(labelHour);
			Controls.Add(labelMinute);
			Controls.Add(labelSecond);
			reset();
		}
	}

	static class Program
	{

		public static void Main(string[] args)
		{
			Application.Run(new MainForm());
		}
	}
}
