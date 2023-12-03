using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Econtact.ContactBook;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ContactFormProject
{
    public partial class Form1 : Form
    {
        
        
        Contacts contact = new Contacts();
        public Form1()
        {
            InitializeComponent();
          
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxfirstName_Enter(object sender, EventArgs e)
        {
            if(textBoxfirstName.Text == "Enter your first name")
            {
                textBoxfirstName.Text = "";
                textBoxfirstName.ForeColor = Color.Black;
            }
        }

        private void textBoxfirstName_Leave(object sender, EventArgs e)
        {
            if (textBoxfirstName.Text == "")
            {
                textBoxfirstName.Text = "Enter your first name";
                textBoxfirstName.ForeColor = Color.Black;
            }
        }

        private void textBoxLastName_Enter(object sender, EventArgs e)
        {
            if (textBoxLastName.Text == "Enter your last name")
            {
                textBoxLastName.Text = "";
                textBoxLastName.ForeColor = Color.Black;
            }
        }

        private void textBoxLastName_Leave(object sender, EventArgs e)
        {
            if (textBoxLastName.Text == "")
            {
                textBoxLastName.Text = "Enter your last name";
                textBoxLastName.ForeColor = Color.Black;
            }
        }


        private void textBoxPhoneNo_Enter(object sender, EventArgs e)
        {
            if (textBoxPhoneNo.Text == "Enter your phone number")
            {
                textBoxPhoneNo.Text = "";
                textBoxPhoneNo.ForeColor = Color.Black;
            }
        }

        private void textBoxPhoneNo_Leave(object sender, EventArgs e)
        {
            if (textBoxPhoneNo.Text == "")
            {
                textBoxPhoneNo.Text = "Enter your phone number";
                textBoxPhoneNo.ForeColor = Color.Black;
            }
        }

        private void textBoxAddress_Enter(object sender, EventArgs e)
        {
            if (textBoxAddress.Text == "Enter your full address")
            {
                textBoxAddress.Text = "";
                textBoxAddress.ForeColor = Color.Black;
            }
        }

        private void textBoxAddress_Leave(object sender, EventArgs e)
        {
            if (textBoxAddress.Text == "")
            {
                textBoxAddress.Text = "Enter your full address";
                textBoxAddress.ForeColor = Color.Black;
            }
        }

        private void textBoxContactID_Enter(object sender, EventArgs e)
        {
            if (textBoxContactID.Text == "Enter contact ID")
            {
                textBoxContactID.Text = "";
                textBoxContactID.ForeColor = Color.Black;
            }
        }

        private void textBoxContactID_MouseEnter(object sender, EventArgs e)
        {
            if (textBoxAddress.Text == "Enter your contact ID")
            {
                textBoxAddress.Text = "";
                textBoxAddress.ForeColor = Color.Black;
            }
        }
        
        private void textBoxContactID_Leave(object sender, EventArgs e)
        {
            if (textBoxContactID.Text == "")
            {
                textBoxContactID.Text = "Enter contact ID";
                textBoxContactID.ForeColor = Color.Black;
            }
        }
        private void comboBoxGender_Leave(object sender, EventArgs e)
        {

        }
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            DataTable dt = contact.Select();
            dataGridView1.DataSource = dt;
        }
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            Environment.Exit(0);
        }
        // ********************* Adding Data (Add Button) ****************
        private void btnAdd_Click(object sender, EventArgs e)
        {
            contact.contactID = int.Parse(textBoxContactID.Text);
            contact.firstName = textBoxfirstName.Text;
            contact.lastName = textBoxLastName.Text;
            contact.contactNo = textBoxPhoneNo.Text;
            contact.address = textBoxAddress.Text;
            contact.gender = comboBoxGender.Text;
            bool value = contact.InsertContact();

            if (value == true)
            {
                this.refreshDataGrid();
                //Update Succesfully
                MessageBox.Show("Added Succesfully");
            }
            else
            {
                //Failed
                MessageBox.Show("Failed to Add (Error) !!!");
            }
            dataGridView1.DataSource = contact.Select();
        }
        private void comboBoxGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // ************** Updating Data (Update button functionality *****************
        private void button4_Click(object sender, EventArgs e)
        {
            contact.contactID = int.Parse(textBoxContactID.Text);
            contact.firstName = textBoxfirstName.Text;
            contact.lastName = textBoxLastName.Text;
            contact.contactNo = textBoxPhoneNo.Text;
            contact.address = textBoxAddress.Text;
            contact.gender = comboBoxGender.Text;
            bool value = contact.updateContact();
            if (value == true )
            {
                this.refreshDataGrid();
                //Update Succesfully
                 MessageBox.Show("Updated Succesfully");
            }
            else
            {
                //Failed
                MessageBox.Show("Failed to Update (Error) !!!");
            }
            this.clearText();
        }
       
        //************ Clear Button Functionality *************
        private void clearText()
        {
            textBoxContactID.Text = "";
            textBoxfirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxPhoneNo.Text = "";
            textBoxAddress.Text = "";
            comboBoxGender.Text = "";


            this.textBoxContactID_Leave(this.textBoxContactID, EventArgs.Empty);
            this.textBoxfirstName_Leave(this.textBoxfirstName, EventArgs.Empty);
            this.textBoxLastName_Leave(this.textBoxLastName, EventArgs.Empty);
            this.textBoxPhoneNo_Leave(this.textBoxPhoneNo, EventArgs.Empty);
            this.textBoxAddress_Leave(this.textBoxAddress, EventArgs.Empty);
            this.comboBoxGender_Leave(this.comboBoxGender, EventArgs.Empty);

        }
        private void refreshDataGrid()
        {
            dataGridView1.DataSource = contact.Select();
        }


        
         // ****************** Deleting Data ***********************
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure to delete?" , "Delete Document" , MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                contact.contactID = int.Parse(textBoxContactID.Text);
                bool value = contact.deleteContact();
                if (value == true)
                {
                    //Deleted Succesfully
                    MessageBox.Show("Contact Succesfully Deleted");
                    this.refreshDataGrid();
                }
                else
                {
                    //Failed to Delete
                    MessageBox.Show("Something Wrong (Failed to Delete) !!!");
                }
            }
            this.clearText();
        }
        // Clear functionality
        private void btnClear_Click(object sender, EventArgs e)
        {
            textBoxContactID.Text = "";
            textBoxfirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxPhoneNo.Text = "";
            textBoxAddress.Text = "";
            comboBoxGender.Text = "";


            this.textBoxContactID_Leave(this.textBoxContactID, EventArgs.Empty);
            this.textBoxfirstName_Leave(this.textBoxfirstName, EventArgs.Empty);
            this.textBoxLastName_Leave(this.textBoxLastName, EventArgs.Empty);
            this.textBoxPhoneNo_Leave(this.textBoxPhoneNo, EventArgs.Empty);
            this.textBoxAddress_Leave(this.textBoxAddress, EventArgs.Empty);
            this.comboBoxGender_Leave(this.comboBoxGender, EventArgs.Empty);
        }


        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text;
            dataGridView1.DataSource = contact.search(searchText);
        }

        // Row Header
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get data from grid  view and Load it to the textboxes respectively
            // identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            textBoxContactID.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxfirstName.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxPhoneNo.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxAddress.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
            comboBoxGender.Text = dataGridView1.Rows[rowIndex].Cells[5].Value.ToString();
        }  
    }

}
