using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using System.Data.Objects;

using BaseFormsLib;
using EducServLib;
using PriemLib;

namespace Priem
{
    public delegate void UpdateListHandler();

    public partial class BookCard : BaseCard
    {
        protected DBPriem _bdc;
        public event UpdateListHandler ToUpdateList;

        public BookCard()
        {
            InitializeComponent();
            _Id = null;
        }

        public BookCard(string id)
        {
            InitializeComponent();
            _Id = id;
        }

        protected Guid? GuidId
        {
            get { return new Guid(_Id); }
        }

        protected override void ExtraInit()
        {
            this.MdiParent = MainClass.mainform;
            _isModified = false;
            this.CenterToParent();          

            _bdc = MainClass.Bdc;          
            CardTitle = string.Empty;
        }

        protected override bool IsForReadOnly()
        {
            return MainClass.IsReadOnly();
        }

        protected override bool GetIsOpen()
        {
            return MainClass.GetIsOpen(_tableName, _Id);
        }

        protected override void SetIsOpen()
        {
            MainClass.SetIsOpen(_tableName, _Id);
        }

        protected override void DeleteIsOpen()
        {
            MainClass.DeleteIsOpen(_tableName, _Id);
        }

        protected override void ShowMessageIsOpen()
        {
            string hold = MainClass.GetIsOpenHolder(_tableName, _Id);
            MessageBox.Show(string.Format("Карточка открыта пользователем {0} и поэтому доступна только для чтения", hold), "Ошибка");
        }

        protected virtual bool CheckFields()
        {
            return true;
        }

        protected override bool SaveRecord()
        {
            if (!CheckFields())
                return false;

            try
            {
                if (!IsForReadOnly())
                {
                    string newId = Save();
                    if (_Id == null)
                        _Id = newId;

                    return true;
                }
                return false;
            }
            catch (Exception de)
            {
                WinFormsServ.Error("Ошибка обновления данных" + de.Message);
                return false;
            }
        }

        protected virtual string Save()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    if (_Id == null)
                    {
                        ObjectParameter entId = new ObjectParameter("id", typeof(Guid));
                        using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            try
                            {
                                InsertRec(context, entId);                               
                                SaveManyToMany(context, (Guid)entId.Value);

                                transaction.Complete();
                            }
                            catch (Exception exc)
                            {
                                throw new Exception("Ошибка при сохранении данных: " + exc.Message);
                            }
                        }
                        return entId.Value.ToString();
                    }
                    else
                    {
                        Guid entId = GuidId.Value;
                        using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            try
                            {
                                UpdateRec(context, entId);
                                SaveManyToMany(context, entId);

                                transaction.Complete();
                            }
                            catch (Exception exc)
                            {
                                throw new Exception("Ошибка при сохранении данных: " + exc.Message);
                            }
                        }
                        return entId.ToString();
                    }
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Ошибка при сохранении данных: " + exc.Message);
            }
        }

        protected virtual void SaveManyToMany(PriemEntities context, Guid id)
        {
        }

        protected virtual void InsertRec(PriemEntities context, ObjectParameter idParam)
        {
        }

        protected virtual void UpdateRec(PriemEntities context, Guid id)
        {            
        }       
  
        protected override void OnSave()
        {
            if (ToUpdateList != null)
                ToUpdateList();
        } 
    }
}