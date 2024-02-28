using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef;

public class TareasContext: DbContext
{
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> Tareas {get;set;}

    public TareasContext(DbContextOptions<TareasContext> options) :base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Categoria> categoriasInit = new List<Categoria>(); //Agregando la nueva coleccion de datos
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("ea6202de-5314-4ce0-903b-94fa7c8ef5ed"), Nombre = "Actividades pendientes", Peso = 20});
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("3032c5ab-b477-49a9-83cf-59913875d91d"), Nombre = "Actividades personales", Peso = 50});
        
        modelBuilder.Entity<Categoria>(categoria=> 
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(p=> p.CategoriaId);

            categoria.Property(p=> p.Nombre).IsRequired().HasMaxLength(150);

            categoria.Property(p=> p.Descripcion).IsRequired(false);

            categoria.Property(p=> p.Peso);
        });
        
        List<Tarea> tareasInit = new List<Tarea>();

        tareasInit.Add(new Tarea(){ TareaId = Guid.Parse("ea6202de-5314-4ce0-903b-94fa7c8ef5ed"), CategoriaId = Guid.Parse("d89a0c0f-6b0b-40cf-999a-1922f8a66c86"), PrioridadTarea = Prioridad.Media, Titulo = "Pago de servicios publicos", FechaCreacion = DateTime.Now });
        tareasInit.Add(new Tarea(){ TareaId = Guid.Parse("3032c5ab-b477-49a9-83cf-59913875d91d"), CategoriaId = Guid.Parse("af3cad7b-ad33-41c2-8633-c9203a961d7f"), PrioridadTarea = Prioridad.Baja, Titulo = "Terminar de ver pelicula en Netflix", FechaCreacion = DateTime.Now });

        modelBuilder.Entity<Tarea>(tarea=>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(p=> p.TareaId);

            tarea.HasOne(p=> p.Categoria).WithMany(p=> p.Tareas).HasForeignKey(p=> p.CategoriaId);

            tarea.Property(p=> p.Titulo).IsRequired().HasMaxLength(200);

            tarea.Property(p=> p.Descripcion).IsRequired(false);

            tarea.Property(p=> p.PrioridadTarea);

            tarea.Property(p=> p.FechaCreacion);

            tarea.Ignore(p=> p.Resumen);

        });

    }

}