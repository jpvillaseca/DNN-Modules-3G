-- =============================================
-- Author:		Juan Pablo Villaseca
-- Create date: 2015-07-29
-- Description:	Transforma una lista de parámetros en una tabla temporal
-- =============================================
CREATE function [dbo].[f_split]
(
@param nvarchar(max), 
@delimiter char(1)
)
	returns @t table (val nvarchar(max), seq int)
	as
	begin
	set @param += @delimiter

	;with a as
	(
	select cast(1 as bigint) f, charindex(@delimiter, @param) t, 1 seq
	union all
	select t + 1, charindex(@delimiter, @param, t + 1), seq + 1
	from a
	where charindex(@delimiter, @param, t + 1) > 0
	)
	insert @t
	select substring(@param, f, t - f), seq from a
	option (maxrecursion 0)
	return
end

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Juan Pablo Villaseca
-- Create date: 2015-07-29
-- Description:	Busca si un ani está suscrito a algún ID Producto
-- =============================================
CREATE PROCEDURE IsAniSuscribedProducto 
	-- Add the parameters for the stored procedure here
	@ani nvarchar(100) = '',
	@id_productos nvarchar(200) = '',
	@estado smallint = 0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS (SELECT TOP 1 1
		FROM lista_integrante WITH (NOLOCK) 
		WHERE id_lista in
			(SELECT id_lista
			FROM campaña_lista 
			WHERE id_campaña IN
				(SELECT id_campaña
					FROM [Clientes].[dbo].[campaña]
					WHERE contrato_pro  IN
					(SELECT val FROM f_split(@id_productos, ','))
				)
			)
			AND id_cliente IN 
			(SELECT id_cliente 
			FROM cliente WITH (NOLOCK) 
			WHERE ani = @ani)
	 AND DATEDIFF(dd,fechahora,GETDATE())= 0)
	BEGIN
		SET @estado = 1
	END
END
GO
