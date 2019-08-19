using Library.Data.Entities.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Maps.Test
{
	public class TestMap : IEntityTypeConfiguration<TestClass>
	{
		public void Configure(EntityTypeBuilder<TestClass> builder)
		{
			//Define primary key
			builder.HasKey(a => a.Id);
		}
	}
}
