using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts
{
    public class BusinessLogicLayer
    {
        private DataAccessLayer _dataAccessLayer;

        public BusinessLogicLayer()
        {
            _dataAccessLayer = new DataAccessLayer();
        }

        public Contact SaveContact(Contact contact)
        {
            if (contact.Id == 0)
            {
                _dataAccessLayer.InsertContact(contact);
            }
            
            else
            {
                _dataAccessLayer.UpdateContact(contact);
            }
            return contact;
                
            
        }

        public List<Contact> GetContact(string search = null)
        {
            return _dataAccessLayer.GetContact(search);
        }

        public void DeleteContact(int id)
        {
            _dataAccessLayer.DeleteContact(id);
        }
    }
}
