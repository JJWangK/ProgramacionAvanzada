namespace PAP.Generators
{
    /// <summary>
    /// Estado de un generador en la pantalla Admin.
    /// </summary>
    public class GeneratorStatus
    {
        public GeneratorStatus(string tableName, bool isEnabled, string message)
        {
            TableName = tableName;
            IsEnabled = isEnabled;
            Message = message;
        }

        public string TableName { get; private set; }

        /// <summary>
        /// Indica si el botón Generar debe estar habilitado.
        /// </summary>
        public bool IsEnabled { get; private set; }

        /// <summary>
        /// Motivo del estado actual.
        /// </summary>
        public string Message { get; private set; }
    }
}
