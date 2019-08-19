using System;

namespace Library.Data.Entities
{
	public class BaseEntity : IEntity
	{
		public BaseEntity()
		{
			this.CreatedOn = DateTime.Now;
			this.UpdatedOn = DateTime.Now;
		}


		public Int32 Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
	}
}
