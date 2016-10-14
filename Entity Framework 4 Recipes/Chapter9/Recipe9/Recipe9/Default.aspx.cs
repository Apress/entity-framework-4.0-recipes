using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recipe9
{
    public partial class Default : System.Web.UI.Page
    {
        public Job Job
        {
            get
            {
                var bytes = ViewState["job"] as byte[];
                return ByteArraySerializer.ToObject<Job>(bytes);
            }

            set
            {
                var bytes = ByteArraySerializer.ToByteArray<Job>(value);
                ViewState["job"] = bytes;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // create the default job
                this.Job = CreateJob("Plumber", 82000M);
                InitializeControls();
            }

        }

        private void InitializeControls()
        {
            this.JobTitleLabel.Text = Job.Title;
            this.SalaryLabel.Text = Job.Salary.ToString();
        }

        private Job CreateJob(string title, decimal salary)
        {
            using (var context = new EFRecipesEntities())
            {
                return new Job { Title = title, Salary = salary };
            }
        }

        protected void Create_Click(object sender, EventArgs e)
        {
            decimal salary = 0;
            decimal.TryParse(this.Salary.Text, out salary);

            this.Job = CreateJob(this.JobTitle.Text, salary);
            InitializeControls();
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            decimal salary = 0;
            decimal.TryParse(this.Salary.Text, out salary);

            this.Job = CreateJob(this.JobTitle.Text, salary);
            InitializeControls();
        }
    }
}