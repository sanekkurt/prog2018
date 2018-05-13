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
        List<int> tempNumberPizza = new List<int>();
        List<NamePizza> namePizzas = new List<NamePizza>();
        List<int> tempNumberDrinks = new List<int>();
        List<NameDrink> nameDrinks = new List<NameDrink>();
        List<int> tempNumberSauces = new List<int>();
        List<NameSauce> nameSauces = new List<NameSauce>();

        public Form1()
        {
            InitializeComponent();
            comboBox2.Enabled = false;
            numericUpDown2.Enabled = false;
            button4.Enabled = false;
            dataGridView2.Enabled = false;
            comboBox3.Enabled = false;
            numericUpDown3.Enabled = false;
            button5.Enabled = false;
            dataGridView3.Enabled = false;
            comboBox1.DataSource = Enum.GetValues(typeof(NamePizza));
            comboBox2.DataSource = Enum.GetValues(typeof(NameDrink));
            comboBox3.DataSource = Enum.GetValues(typeof(NameSauce));
            comboBox4.DataSource = Enum.GetValues(typeof(Currency));
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView3.AllowUserToAddRows = false;
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
            numericUpDown2.Enabled = checkBox1.Checked;
            button4.Enabled = checkBox1.Checked;
            dataGridView2.Enabled = checkBox1.Checked;
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
            textBox3.Text = dto.Price.ToString();
            tempNumberPizza = dto.Pizza.numberPizza;
            namePizzas = dto.Pizza.NamePizza;
            tempNumberDrinks = dto.Pizza.additions.numberDrink;
            nameDrinks = dto.Pizza.additions.Drink;
            tempNumberSauces = dto.Pizza.additions.numberSauce;
            nameSauces = dto.Pizza.additions.Sauce;
            checkBox1.Checked = dto.Pizza.additions.DrinkCheck;
            checkBox2.Checked = dto.Pizza.additions.SauceCheck;
            comboBox4.SelectedItem = dto.Currency;
            
            for(int i =0; i < tempNumberPizza.Count; i++)
            {
                int rowNumber = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["Column1"].Value = namePizzas[i];
                dataGridView1.Rows[i].Cells["Column2"].Value = tempNumberPizza[i];
            }
            for (int i = 0; i < tempNumberDrinks.Count; i++)
            {
                int rowNumber = dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells["Column3"].Value = nameDrinks[i];
                dataGridView2.Rows[i].Cells["Column4"].Value = tempNumberDrinks[i];
            }
            for (int i = 0; i < tempNumberSauces.Count; i++)
            {
                int rowNumber = dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells["Column5"].Value = nameSauces[i];
                dataGridView3.Rows[i].Cells["Column6"].Value = tempNumberSauces[i];
            }
        }
        Order GetModelFromUI()
        {
            return new Order()
            {
                dateTime = dateTimePicker1.Value,
                FullNameCustomer = textBox1.Text,
                Address = textBox2.Text,
                Price = double.Parse(textBox3.Text),
                Pizza=PizzaRequirements(),
                Currency = (PizzaOrder.Currency)comboBox4.SelectedValue
            };
        }
        PizzaRequirements PizzaRequirements()
        {
            return new PizzaRequirements()
            {
                numberPizza = tempNumberPizza,
                NamePizza = namePizzas,
                additions = new Addition
                {
                    SauceCheck = checkBox2.Checked,
                    numberSauce = tempNumberSauces,
                    Sauce = nameSauces,
                    DrinkCheck = checkBox1.Checked,
                    numberDrink = tempNumberDrinks,
                    Drink = nameDrinks,
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

        private void button3_Click(object sender, EventArgs e)
        {
            int rowNumber = dataGridView1.Rows.Add();
            dataGridView1.Rows[rowNumber].Cells["Column1"].Value = comboBox1.SelectedValue;
            dataGridView1.Rows[rowNumber].Cells["Column2"].Value = numericUpDown1.Value;
            tempNumberPizza.Add((int)numericUpDown1.Value);
            namePizzas.Add((PizzaOrder.NamePizza)comboBox1.SelectedValue);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = checkBox2.Checked;
            numericUpDown3.Enabled = checkBox2.Checked;
            button5.Enabled = checkBox2.Checked;
            dataGridView3.Enabled = checkBox2.Checked;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowNumber = dataGridView2.Rows.Add();
            dataGridView2.Rows[rowNumber].Cells["Column3"].Value = comboBox2.SelectedValue;
            dataGridView2.Rows[rowNumber].Cells["Column4"].Value = numericUpDown2.Value;
            tempNumberDrinks.Add((int)numericUpDown2.Value);
            nameDrinks.Add((PizzaOrder.NameDrink)comboBox2.SelectedValue);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int rowNumber = dataGridView3.Rows.Add();
            dataGridView3.Rows[rowNumber].Cells["Column5"].Value = comboBox3.SelectedValue;
            dataGridView3.Rows[rowNumber].Cells["Column6"].Value = numericUpDown3.Value;
            tempNumberSauces.Add((int)numericUpDown3.Value);
            nameSauces.Add((PizzaOrder.NameSauce)comboBox3.SelectedValue);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            numericUpDown3.Value = 1;
            while (dataGridView1.Rows.Count != 0)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.Rows.Count - 1]);
            }

            if (checkBox1.Checked)
            {
                while (dataGridView2.Rows.Count != 0)
                {
                    dataGridView2.Rows.Remove(dataGridView2.Rows[dataGridView2.Rows.Count - 1]);
                }
            }
            if (checkBox2.Checked)
            {
                while (dataGridView3.Rows.Count != 0)
                {
                    dataGridView3.Rows.Remove(dataGridView3.Rows[dataGridView3.Rows.Count - 1]);
                }
            }
            checkBox1.Checked = false;
            checkBox2.Checked = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var lv = new LicenceValidator();
            if (!lv.HasLicense)
            {
                MessageBox.Show("Лицензия не найдена. Приобретите ее у разработчика.");
                Application.Exit();
            }
            if (!lv.IsValid)
            {
                MessageBox.Show("Срок действия лицензии окончен. Приобретите ее у разработчика.");
                Application.Exit();
            }
        }
    }
    
}
