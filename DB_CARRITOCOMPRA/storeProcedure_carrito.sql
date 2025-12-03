use DBCarrito
go

create proc sp_RegistrarUsuario (
	@nombres varchar(100),
	@apellidos varchar(100),
	@correo varchar(100),
	@clave varchar(150),
	@activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
as
begin
	set @Resultado = 0

	if not exists (select * from USUARIO where correo = @correo)
		begin
			insert into USUARIO (nombres, apellidos, correo, clave, activo) 
					values(@nombres, @apellidos, @correo, @clave,@activo)

					set @Resultado = SCOPE_IDENTITY()
		end
	else
		set @Mensaje = 'El correo del usuario ya existe'
end
go

create proc sp_EditarUsuario (
	@idUsuario int,
	@nombres varchar(100),
	@apellidos varchar(100),
	@correo varchar(100),
	@activo bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
as
begin
	set @Resultado = 0
	if not exists (select * from USUARIO where correo = @correo and idUsuario != @idUsuario)
		begin
			update top(1) USUARIO set
				nombres = @nombres,
				apellidos = @apellidos,
				correo = @correo,
				activo = @activo
			where idUsuario = @idUsuario

			set @Resultado = 1
		end
	else
		set @Mensaje = 'El correo del usuario ya existe'
end
go