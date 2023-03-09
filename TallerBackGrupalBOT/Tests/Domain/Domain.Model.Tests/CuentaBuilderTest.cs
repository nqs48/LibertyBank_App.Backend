using Domain.Model.Entities.Cuentas;

namespace Domain.Model.Tests;

public class CuentaBuilderTest
{
    private string _id = string.Empty;
    private string _idCliente = string.Empty;
    private string _numeroCuenta = string.Empty;
    private TipoCuenta _tipoCuenta;
    private EstadoCuenta _estadoCuenta;
    private decimal _saldo = 0;
    private decimal _saldoDisponible = 0;
    private bool _exenta;

    public CuentaBuilderTest()
    {
    }

    public CuentaBuilderTest WithId(string id)
    {
        _id = id;
        return this;
    }

    public CuentaBuilderTest WithIdCliente(string id)
    {
        _idCliente = id;
        return this;
    }

    public CuentaBuilderTest WithNumeroDeCuenta(string numeroDeCuenta)
    {
        _numeroCuenta = numeroDeCuenta;
        return this;
    }

    public CuentaBuilderTest WithTipoCuenta(TipoCuenta tipoCuenta)
    {
        _tipoCuenta = tipoCuenta;
        return this;
    }

    public CuentaBuilderTest WithEstadoCuenta(EstadoCuenta estadoCuenta)
    {
        _estadoCuenta = estadoCuenta;
        return this;
    }

    public CuentaBuilderTest WithSaldo(decimal saldo)
    {
        _saldo = saldo;
        return this;
    }

    public CuentaBuilderTest WithSaldoDisponible(decimal saldoDisponible)
    {
        _saldoDisponible = saldoDisponible;
        return this;
    }

    public CuentaBuilderTest WithExenta(bool exenta)
    {
        _exenta = exenta;
        return this;
    }

    public Cuenta Build() => new(_id, _idCliente, _numeroCuenta, _tipoCuenta, _saldo, _saldoDisponible, _exenta);
}