using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using paymentDomain.Entities;

namespace paymentInfrastructure.Persistence
{
    public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.IntegrationEvents);
        }
    }
}
