namespace PAP.Generators
{
    /// <summary>
    /// Contrato común de los generadores de datos de prueba.
    /// </summary>
    public interface IDataGenerator
    {
        /// <summary>
        /// Nombre de la tabla tal como se muestra en la pantalla Admin.
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// Indica si la tabla ya tiene registros.
        /// </summary>
        bool HasData();

        /// <summary>
        /// Genera los registros de prueba y devuelve cuántos insertó.
        /// </summary>
        int Generate();
    }
}
