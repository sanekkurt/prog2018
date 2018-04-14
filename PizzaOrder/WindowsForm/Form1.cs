using System;
using PizzaOrder;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox2.Enabled = false;
            comboBox1.DataSource = Enum.GetValues(typeof(NamePizza));
            comboBox2.DataSource = Enum.GetValues(typeof(Drink));
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = checkBox1.Checked;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog() { Filter = "Файлы заказов|*.pizza" };
            var result = sfd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var dto = GetModelFromUI();
                RideDtoHelper.WriteToFile(sfd.FileName, dto);
            }
        }
        private void SetModelToUI(Order dto)
        {
            dateTimePicker1.Value = dto.dateTime;
            textBox1.Text = dto.FullNameCustomer;
            textBox2.Text = dto.Address;
            numericUpDown1.Value = dto.Pizza.number;
            comboBox1.SelectedItem = dto.Pizza.NamePizza;
            checkBox1.Checked = dto.Pizza.additions.DrinkCheck;
            checkBox2.Checked = dto.Pizza.additions.Sauce;
            comboBox2.SelectedItem = dto.Pizza.additions.Drink;
        }
        Order GetModelFromUI()
        {
            return new Order()
            {
                dateTime = dateTimePicker1.Value,
                FullNameCustomer = textBox1.Text,
                Address = textBox2.Text,
                Pizza=PizzaRequirements()
            };
        }
        PizzaRequirements PizzaRequirements()
        {
            return new PizzaRequirements()
            {
                number = (int)numericUpDown1.Value,
                NamePizza = (PizzaOrder.NamePizza)comboBox1.SelectedValue,
                additions = new Addition
                {
                    Sauce = checkBox2.Checked,
                    DrinkCheck = checkBox1.Checked,
                    Drink = (PizzaOrder.Drink)comboBox2.SelectedValue
                }
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "Файл заказа|*.pizza" };
            var result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var dto = RideDtoHelper.LoadFromFile(ofd.FileName);
                SetModelToUI(dto);
            }
        }
    }
}
