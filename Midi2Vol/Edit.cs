using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Midi2Vol
{
    public partial class Edit : Form
    {
        private List<App> apps;
        App currentApp;
        bool save = false;
        public Edit(List<App> apps)
        {
            this.apps = apps;
            InitializeComponent();
            foreach (App app in apps)
            {
                listBox1.Items.Add(app.name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save = true;
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                foreach (App app in apps)
                {
                    if (app.name == textBox1.Text)
                    {
                        app.name = textBox1.Text;
                        app.AppRaw = textBox2.Text;
                        app.ProcessName = textBox3.Text;
                        return;

                    }

                }
            }
            // app not found save it

            App newApp = new App(textBox1.Text, textBox2.Text, textBox3.Text);
            apps.Add(newApp);
            listBox1.Items.Add(textBox1.Text);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                String name = listBox1.SelectedItem.ToString();
                listBox1.ClearSelected();
                apps.Remove(currentApp);
                listBox1.Items.Remove(currentApp.name);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                foreach (App app in apps)
                {
                    if (app.name == listBox1.SelectedItem.ToString())
                    {
                        currentApp = app;
                        textBox1.Text = app.name;
                        textBox2.Text = app.AppRaw;
                        textBox3.Text = app.ProcessName;
                    }
                }
                textBox1.Update();
                textBox2.Update();
                textBox3.Update();
            }
        }

        private void Edit_Load(object sender, EventArgs e)
        {

        }

        public bool  getSave()
        {
            return save;
        }
    }
}
