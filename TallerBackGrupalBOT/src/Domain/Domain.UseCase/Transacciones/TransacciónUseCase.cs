using credinet.exception.middleware.models;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Transacciones;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils;
using Helpers.ObjectsUtils.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.UseCase.Transacciones
{
    public class TransacciónUseCase : ITransacciónUseCase
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly ITransacciónRepository _transacciónRepository;
        private readonly IOptions<ConfiguradorAppSettings> _options;

        public TransacciónUseCase(ICuentaRepository cuentaRepository,
            ITransacciónRepository transacciónRepository,
            IOptions<ConfiguradorAppSettings> options)
        {
            _cuentaRepository = cuentaRepository;
            _transacciónRepository = transacciónRepository;
            _options = options;
        }

        public async Task<Transacción> ObtenerTransacciónPorId(string idTransacción) =>
                        await _transacciónRepository.ObtenerPorId(idTransacción);

        public async Task<List<Transacción>> ObtenerTransaccionesPorIdCuenta(string idCuenta) =>
                        await _transacciónRepository.ObtenerPorIdCuenta(idCuenta);

        public async Task<Transacción> RealizarConsignación(Transacción transacción)
        {
            var cuenta = await _cuentaRepository.ObtenerPorId(transacción.IdCuenta);

            //TODO: Validar Cuenta Cancelada

            transacción.AsignarTipoTransacción(TipoTransacción.Consignación);
            transacción.AsignarFechaMovimiento();
            transacción.AsignarSaldoInicial(cuenta.Saldo);
            transacción.AsignarSaldoFinalCredito(transacción.Valor);
            transacción.GenerarDescripción();

            //TODO: Actualizar Saldo de la cuenta
            await _cuentaRepository.Actualizar(transacción.Id, cuenta);
            return await _transacciónRepository.Crear(transacción);
        }

        public async Task<Transacción> RealizarRetiro(Transacción transacción)
        {
            var cuenta = await _cuentaRepository.ObtenerPorId(transacción.IdCuenta);
            var valorRetiro = ValidarValorRetiro(transacción.Valor, cuenta);

            //TODO: Validar Cuenta Cancelada
            //TODO: Validar Cuenta Inhabilitada

            transacción.AsignarTipoTransacción(TipoTransacción.Retiro);
            transacción.AsignarFechaMovimiento();
            transacción.AsignarSaldoInicial(cuenta.Saldo);
            transacción.AsignarSaldoFinalDebito(valorRetiro);
            transacción.GenerarDescripción();

            //TODO: Actualizar Saldo de la cuenta
            await _cuentaRepository.Actualizar(transacción.Id, cuenta);
            return await _transacciónRepository.Crear(transacción);
        }

        public async Task<Transacción> RealizarTransferencia(Transacción transacción, string idCuentaReceptor)
        {
            var cuentaOrigen = await _cuentaRepository.ObtenerPorId(transacción.IdCuenta);
            var cuentaDestino = await _cuentaRepository.ObtenerPorId(idCuentaReceptor);

            var valorRetiro = ValidarValorRetiro(transacción.Valor, cuentaOrigen);

            transacción.AsignarTipoTransacción(TipoTransacción.Transferencia);
            transacción.AsignarFechaMovimiento();
            transacción.AsignarSaldoInicial(cuentaOrigen.Saldo);
            transacción.AsignarSaldoFinalDebito(valorRetiro);
            transacción.GenerarDescripción(idCuentaReceptor);

            var transacciónReceptor = CrearTransacciónReceptor(transacción, cuentaDestino);

            //TODO: Actualizar Saldo de las 2 cuentas

            await _transacciónRepository.Crear(transacciónReceptor);
            return await _transacciónRepository.Crear(transacción);
        }

        private decimal ValidarValorRetiro(decimal valor, Cuenta cuenta)
        {
            var valorConGMF = valor * (1.0M + _options.Value.GMF);
            var saldoConSobregiro = cuenta.Saldo + _options.Value.ValorSobregiro;

            if (cuenta.Exenta)
            {
                if (cuenta.TipoCuenta == TipoCuenta.Ahorros && valor > cuenta.Saldo)
                {
                    throw new BusinessException(TipoExcepcionNegocio.ValorRetiroNoPermitido.GetDescription(),
                                (int)TipoExcepcionNegocio.ValorRetiroNoPermitido);
                }

                if (cuenta.TipoCuenta == TipoCuenta.Corriente && valor > saldoConSobregiro)
                {
                    throw new BusinessException(TipoExcepcionNegocio.ValorRetiroNoPermitido.GetDescription(),
                                (int)TipoExcepcionNegocio.ValorRetiroNoPermitido);
                }

                return valor;
            }

            if (cuenta.TipoCuenta == TipoCuenta.Ahorros && valorConGMF > cuenta.Saldo)
            {
                throw new BusinessException(TipoExcepcionNegocio.ValorRetiroNoPermitido.GetDescription(),
                            (int)TipoExcepcionNegocio.ValorRetiroNoPermitido);
            }

            if (cuenta.TipoCuenta == TipoCuenta.Corriente && valorConGMF > saldoConSobregiro)
            {
                throw new BusinessException(TipoExcepcionNegocio.ValorRetiroNoPermitido.GetDescription(),
                            (int)TipoExcepcionNegocio.ValorRetiroNoPermitido);
            }

            return valorConGMF;
        }

        private Transacción CrearTransacciónReceptor(Transacción transacción, Cuenta cuentaReceptor)
            => new(cuentaReceptor.Id,
                transacción.FechaMovimiento,
                TipoTransacción.Transferencia,
                transacción.Valor,
                cuentaReceptor.Saldo,
                AsignarSaldoFinalReceptor(cuentaReceptor.Saldo, transacción.Valor),
                GenerarDescripciónTransferenciaReceptor(transacción.Valor, transacción.IdCuenta, cuentaReceptor.Id)
            );

        private decimal AsignarSaldoFinalReceptor(decimal saldoInicial, decimal valor) => saldoInicial + valor;

        private String GenerarDescripciónTransferenciaReceptor(decimal valor, string idCuentaOrigen, string idCuentaDestino) =>
            $"Se Recibió Transferencia por ${valor} desde la {idCuentaOrigen} a la cuenta {idCuentaDestino}";
    }
}