using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Configuratons
{
    class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a=>a.Id);
            builder.HasOne(a => a.Bar)
                .WithOne(b => b.Address)
                .HasForeignKey<Address>(a => a.BarId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
