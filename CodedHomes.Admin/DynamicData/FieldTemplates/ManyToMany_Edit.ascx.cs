using System;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;
using System.Collections.Generic;

namespace CodedHomes.Admin
{
    public partial class ManyToMany_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected ObjectContext ObjectContext { get; set; }

        public void Page_Load(object sender, EventArgs e)
        {
            // Register for the DataSource's updating event
            EntityDataSource ds = (EntityDataSource)this.FindDataSourceControl();


            ds.ContextCreated += (_, ctxCreatedEnventArgs) => ObjectContext = ctxCreatedEnventArgs.Context;
            // This field template is used both for Editing and Inserting
            ds.Updating += new EventHandler<EntityDataSourceChangingEventArgs>(DataSource_UpdatingOrInserting);
            ds.Inserting += new EventHandler<EntityDataSourceChangingEventArgs>(DataSource_UpdatingOrInserting);
        }

        //void DataSource_UpdatingOrInserting(object sender, EntityDataSourceChangingEventArgs e)
        //{
        //    MetaTable childTable = ChildrenColumn.ChildTable;

        //    // Comments assume employee/territory for illustration, but the code is generic

        //    // Get the collection of territories for this employee
        //    RelatedEnd entityCollection = (RelatedEnd)Column.EntityTypeProperty.GetValue(e.Entity, null);

        //    // In Edit mode, make sure it's loaded (doesn't make sense in Insert mode)
        //    if (Mode == DataBoundControlMode.Edit && !entityCollection.IsLoaded)
        //    {
        //        entityCollection.Load();
        //    }

        //    // Get an IList from it (i.e. the list of territories for the current employee)
        //    IList entityList = ((IListSource)entityCollection).GetList();

        //    // Go through all the territories (not just those for this employee)
        //    foreach (object childEntity in childTable.GetQuery(e.Context))
        //    {

        //        // Check if the employee currently has this territory
        //        bool isCurrentlyInList = entityList.Contains(childEntity);

        //        // Find the checkbox for this territory, which gives us the new state
        //        string pkString = childTable.GetPrimaryKeyString(childEntity);
        //        ListItem listItem = CheckBoxList1.Items.FindByValue(pkString);
        //        if (listItem == null)
        //            continue;

        //        // If the states differs, make the appropriate add/remove change
        //        if (listItem.Selected)
        //        {
        //            if (!isCurrentlyInList)
        //                entityList.Add(childEntity);
        //        }
        //        else
        //        {
        //            if (isCurrentlyInList)
        //                entityList.Remove(childEntity);
        //        }
        //    }
        //}

        void DataSource_UpdatingOrInserting(object sender, EntityDataSourceChangingEventArgs e)
        {
            MetaTable childTable = ChildrenColumn.ChildTable;

            if (Mode == DataBoundControlMode.Edit)
            {
                ObjectContext.LoadProperty(e.Entity, Column.Name);
            }

            dynamic entityCollection = Column.EntityTypeProperty.GetValue(e.Entity, null);

            foreach (dynamic childEntity in childTable.GetQuery(e.Context))
            {
                var isCurrentlyInList = ListContainsEntity(childTable, entityCollection, childEntity);

                string pkString = childTable.GetPrimaryKeyString(childEntity);
                ListItem listItem = CheckBoxList1.Items.FindByValue(pkString);
                if (listItem == null)
                    continue;

                if (listItem.Selected)
                {
                    if (!isCurrentlyInList)
                        entityCollection.Add(childEntity);
                }
                else
                {
                    if (isCurrentlyInList)
                        entityCollection.Remove(childEntity);
                }
            }
        }
        private static bool ListContainsEntity(MetaTable table, IEnumerable<object> list, object entity)
        {
            return list.Any(e => AreEntitiesEqual(table, e, entity));
        }
        private static bool AreEntitiesEqual(MetaTable table, object entity1, object entity2)
        {
            return Enumerable.SequenceEqual(table.GetPrimaryKeyValues(entity1), table.GetPrimaryKeyValues(entity2));
        }
        //protected void CheckBoxList1_DataBound(object sender, EventArgs e)
        //{
        //    MetaTable childTable = ChildrenColumn.ChildTable;

        //    // Comments assume employee/territory for illustration, but the code is generic

        //    IList entityList = null;
        //    ObjectContext objectContext = null;

        //    if (Mode == DataBoundControlMode.Edit)
        //    {
        //        object entity;
        //        ICustomTypeDescriptor rowDescriptor = Row as ICustomTypeDescriptor;
        //        if (rowDescriptor != null)
        //        {
        //            // Get the real entity from the wrapper
        //            entity = rowDescriptor.GetPropertyOwner(null);
        //        }
        //        else
        //        {
        //            entity = Row;
        //        }

        //        // Get the collection of territories for this employee and make sure it's loaded
        //        RelatedEnd entityCollection = Column.EntityTypeProperty.GetValue(entity, null) as RelatedEnd;
        //        if (entityCollection == null)
        //        {
        //            throw new InvalidOperationException(String.Format("The ManyToMany template does not support the collection type of the '{0}' column on the '{1}' table.", Column.Name, Table.Name));
        //        }
        //        if (!entityCollection.IsLoaded)
        //        {
        //            entityCollection.Load();
        //        }

        //        // Get an IList from it (i.e. the list of territories for the current employee)
        //        entityList = ((IListSource)entityCollection).GetList();

        //        // Get the current ObjectContext
        //        ObjectQuery objectQuery = (ObjectQuery)entityCollection.GetType().GetMethod(
        //            "CreateSourceQuery").Invoke(entityCollection, null);
        //        objectContext = objectQuery.Context;
        //    }

        //    // Go through all the territories (not just those for this employee)
        //    foreach (object childEntity in childTable.GetQuery(objectContext))
        //    {
        //        MetaTable actualTable = MetaTable.GetTable(childEntity.GetType());
        //        // Create a checkbox for it
        //        ListItem listItem = new ListItem(
        //            actualTable.GetDisplayString(childEntity),
        //            actualTable.GetPrimaryKeyString(childEntity));

        //        // Make it selected if the current employee has that territory
        //        if (Mode == DataBoundControlMode.Edit)
        //        {
        //            listItem.Selected = entityList.Contains(childEntity);
        //        }

        //        CheckBoxList1.Items.Add(listItem);
        //    }
        //}
        protected void CheckBoxList1_DataBound(object sender, EventArgs e)
        {
            MetaTable childTable = ChildrenColumn.ChildTable;

            IEnumerable<object> entityCollection = null;

            if (Mode == DataBoundControlMode.Edit)
            {
                object entity;
                ICustomTypeDescriptor rowDescriptor = Row as ICustomTypeDescriptor;
                if (rowDescriptor != null)
                {
                    entity = rowDescriptor.GetPropertyOwner(null);
                }
                else
                {
                    entity = Row;
                }

                entityCollection = (IEnumerable<object>)Column.EntityTypeProperty.GetValue(entity, null);
                var realEntityCollection = entityCollection as RelatedEnd;
                if (realEntityCollection != null && !realEntityCollection.IsLoaded)
                {
                    realEntityCollection.Load();
                }
            }

            foreach (object childEntity in childTable.GetQuery(ObjectContext))
            {
                ListItem listItem = new ListItem(
                    childTable.GetDisplayString(childEntity),
                    childTable.GetPrimaryKeyString(childEntity));

                if (Mode == DataBoundControlMode.Edit)
                {
                    listItem.Selected = ListContainsEntity(childTable, entityCollection, childEntity);
                }
                CheckBoxList1.Items.Add(listItem);
            }
        }
        public override Control DataControl
        {
            get
            {
                return CheckBoxList1;
            }
        }

    }
}
