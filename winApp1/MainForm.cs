/*
 * Created by SharpDevelop.
 * User: dimon
 * Date: 06.06.2016
 * Time: 21:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace winApp1
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	
	  
	public partial class MainForm : Form
	{
		String net_pach = "";
		String cur_date = "";
		String ver = "";
		String dir = "";

		
		public MainForm()
		{
			InitializeComponent();
		}
		
		void add_log(String text)
		{
			log.Text += text + "\r\n";
		}
		
		void set_var() {
			net_pach = @"\\serversoft\ркк-энергия\релизы";
			cur_date=DateTime.Now.ToString("dd.MM.yyyy");
			ver=textVer.Text.Trim(' ');
			dir=net_pach+@"\3.0.0."+ver+" от "+cur_date;

		}
		
		void Button1Click(object sender, EventArgs e)
		{
			
			if (!log.Text.Equals("")) {
				add_log("----------------------------------");
			}
			
			if (textVer.Text.Trim(' ').Equals("")) {
				add_log("ОШИБКА: Не указан номер релиза");
				return;
			}
			
			set_var();
							
			DirectoryInfo net_pach_dir = new DirectoryInfo(net_pach);
			
			DirectoryInfo new_ver = new DirectoryInfo(dir);
			if (!new_ver.Exists) {
				new_ver.Create();
				add_log("СОЗДАНА ПАПКА" + dir);
			} else {
				add_log("ОШИБКА: папка " + dir + " уже существует");
				return;
			}
			
			try {
				String f1 = net_pach + @"\шаблон\ежедневный отчет по запросам от 02.06.2015.xls";
				String f2 = dir + @"\ежедневный отчет по запросам от " + cur_date + ".xls";
				File.Copy(f1, f2);
				add_log("СКОПИРОВАН ФАЙЛ: " + f2);
			} catch (Exception e1) {
				add_log("ОШИБКА " + e1);
				return;
			}
			
			try {
				String f1 = net_pach + @"\шаблон\описания изменений конфигурации ркк-энергия № 3.0.0.647 от 28.12.2015.xls";
				String f2 = dir + @"\описания изменений конфигурации ркк-энергия № 3.0.0." + ver + " от " + cur_date + ".xls";
				File.Copy(f1, f2);
				add_log("СКОПИРОВАН ФАЙЛ: " + f2);
			} catch (Exception e1) {
				add_log("ОШИБКА " + e1);
				return;
			}
						
			String f = dir + @"\ркк-энергия " + cur_date + ".cf";
			File.Create(f);
			add_log("СОЗДАН ФАЙЛ: " + f);
			
			add_log("ВСЕ ГОТОВО");
			
						
		}
		void Button2Click(object sender, EventArgs e)
		{
			set_var();
			DirectoryInfo dir_rel = new DirectoryInfo(dir);
			if (dir_rel.Exists) {
				Process.Start(dir);
			} else {
				Process.Start(net_pach);
			}
		}
		

	}
}
