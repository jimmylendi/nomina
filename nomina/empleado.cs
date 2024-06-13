
namespace nomina.Class
{
    public class empleado
    {
        public string PrimerNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string NumeroSeguroSocial { get; set; }

        public Empleado(string primerNombre, string apellidoPaterno, string numeroSeguroSocial)
        {
            PrimerNombre = primerNombre;
            ApellidoPaterno = apellidoPaterno;
            NumeroSeguroSocial = numeroSeguroSocial;
        }

        // Método abstracto para calcular ingresos
        public abstract decimal CalcularIngreso();

        public override string ToString()
        {
            return $"{PrimerNombre} {ApellidoPaterno}\nNúmero de seguro social: {NumeroSeguroSocial}";
        }
    }

    public class EmpleadoAsalariado : Empleado
    {
        public decimal SalarioSemanal { get; set; }

        public EmpleadoAsalariado(string primerNombre, string apellidoPaterno, string numeroSeguroSocial, decimal salarioSemanal)
            : base(primerNombre, apellidoPaterno, numeroSeguroSocial)
        {
            SalarioSemanal = salarioSemanal;
        }

        public override decimal CalcularIngreso()
        {
            return SalarioSemanal;
        }

        public override string ToString()
        {
            return $"Empleado asalariado: {base.ToString()}\nSalario semanal: {SalarioSemanal:C}";
        }
    }

    public class EmpleadoPorHoras : Empleado
    {
        public decimal SueldoPorHora { get; set; }
        public decimal HorasTrabajadas { get; set; }

        public EmpleadoPorHoras(string primerNombre, string apellidoPaterno, string numeroSeguroSocial, decimal sueldoPorHora, decimal horasTrabajadas)
            : base(primerNombre, apellidoPaterno, numeroSeguroSocial)
        {
            SueldoPorHora = sueldoPorHora;
            HorasTrabajadas = horasTrabajadas;
        }

        public override decimal CalcularIngreso()
        {
            if (HorasTrabajadas <= 40)
                return SueldoPorHora * HorasTrabajadas;
            else
                return (40 * SueldoPorHora) + ((HorasTrabajadas - 40) * SueldoPorHora * 1.5M);
        }

        public override string ToString()
        {
            return $"Empleado por horas: {base.ToString()}\nSueldo por hora: {SueldoPorHora:C}\nHoras trabajadas: {HorasTrabajadas}";
        }
    }

    public class EmpleadoPorComision : Empleado
    {
        public decimal VentasBrutas { get; set; }
        public decimal TarifaComision { get; set; }

        public EmpleadoPorComision(string primerNombre, string apellidoPaterno, string numeroSeguroSocial, decimal ventasBrutas, decimal tarifaComision)
            : base(primerNombre, apellidoPaterno, numeroSeguroSocial)
        {
            VentasBrutas = ventasBrutas;
            TarifaComision = tarifaComision;
        }

        public override decimal CalcularIngreso()
        {
            return TarifaComision * VentasBrutas;
        }

        public override string ToString()
        {
            return $"Empleado por comisión: {base.ToString()}\nVentas brutas: {VentasBrutas:C}\nTarifa de comisión: {TarifaComision:P}";
        }
    }

    public class EmpleadoBaseMasComision : EmpleadoPorComision
    {
        public decimal SalarioBase { get; set; }

        public EmpleadoBaseMasComision(string primerNombre, string apellidoPaterno, string numeroSeguroSocial, decimal ventasBrutas, decimal tarifaComision, decimal salarioBase)
            : base(primerNombre, apellidoPaterno, numeroSeguroSocial, ventasBrutas, tarifaComision)
        {
            SalarioBase = salarioBase;
        }

        public override decimal CalcularIngreso()
        {
            // Agregar 10% al salario base
            return SalarioBase * 1.1M + base.CalcularIngreso();
        }

        public override string ToString()
        {
            return $"Empleado base más comisión: {base.ToString()}\nSalario base: {SalarioBase:C}";
        }
    }

    public void Main(string[] args)
    {
        var empleados = new List<Empleado>
            {
                new EmpleadoAsalariado("John", "Smith", "111-11-1111", 800.00M),
                new EmpleadoPorHoras("Karen", "Price", "222-22-2222", 16.75M, 42.0M),
                new EmpleadoPorComision("Sue", "Jones", "333-33-3333", 10000.00M, .06M),
                new EmpleadoBaseMasComision("Bob", "Lewis", "444-44-4444", 5000.00M, .04M, 300.00M)
            };

        Console.WriteLine("Empleados procesados en forma polimórfica:\n");

        foreach (var empleado in empleados)
        {
            Console.WriteLine(empleado);
            Console.WriteLine($"Ingresos: {empleado.CalcularIngreso():C}\n");
        }
    }
}
