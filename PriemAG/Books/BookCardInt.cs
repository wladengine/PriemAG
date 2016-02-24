using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;
using System.Transactions;
using PriemLib;

namespace Priem
{
    public partial class BookCardInt : BookCard
    {
        public BookCardInt()
        {
            InitializeComponent();
            _Id = null;
        }
        
        public BookCardInt(string id)
        {
            InitializeComponent();
            _Id = id;
        }

        protected int? IntId
        {
            get 
            {
                if (_Id == null)
                    return null;
                else
                    return int.Parse(_Id); 
            }
        }

        protected override string Save()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    if (_Id == null)
                    {
                        ObjectParameter entId = new ObjectParameter("id", typeof(int));
                        using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            try
                            {
                                InsertRec(context, entId);
                                SaveManyToMany(context, (int)entId.Value);

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
                        int entId = IntId.Value;
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

        protected virtual void SaveManyToMany(PriemEntities context, int id)
        {
        }

        protected virtual void InsertRec(PriemEntities context, ObjectParameter idParam)
        {
        }

        protected virtual void UpdateRec(PriemEntities context, int id)
        {
        } 
    }
}
