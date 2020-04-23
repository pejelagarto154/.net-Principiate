using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts
{
    public partial class Main : Form
    {
        private BusinessLogicLayer _businessLogicLayer;
        public Main()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }

        #region events
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContactDetailDialog();
        }
        #endregion

        #region private method
        private void OpenContactDetailDialog()
        {
            ContactDetails ContactDetails = new ContactDetails();
            ContactDetails.ShowDialog(this);
        }
        #endregion

        private void Main_Load(object sender, EventArgs e)
        {
            PopulateContact();
        }

        public void PopulateContact(string search=null)
        {
            List<Contact> contact = _businessLogicLayer.GetContact(search);
            gridContact.DataSource = contact;
        }

        private void gridContact_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContact.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if(cell.Value.ToString()=="Edit")
            {
                ContactDetails contactDetails = new ContactDetails();
                contactDetails.LoadContact(new Contact
                {
                    Id = int.Parse(gridContact.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FirstName = gridContact.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    LastName = gridContact.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Phone = gridContact.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Address = gridContact.Rows[e.RowIndex].Cells[4].Value.ToString()
                });
                contactDetails.ShowDialog(this);
            }
            else if (cell.Value.ToString() == "Delete")
            {
                DeleteContact(int.Parse(gridContact.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateContact();
            }
        }

        private void DeleteContact(int id)
        {
            _businessLogicLayer.DeleteContact(id);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateContact(txtSearch.Text);
            txtSearch.Text = string.Empty;
        }
    }
}
