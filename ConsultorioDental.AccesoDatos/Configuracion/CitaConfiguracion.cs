using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ConsultorioDental.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioDental.AccesoDatos.Configuracion
{
    public class CitaConfiguracion : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Fecha).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Hora).IsRequired().HasMaxLength(100);
            builder.Property(x => x.TratamientoId).IsRequired();
            builder.Property(x => x.UsuarioAplicacionId).IsRequired();
            builder.Property(x => x.Estado).IsRequired();
        }
    }
}
