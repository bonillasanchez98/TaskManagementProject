namespace TaskManagementSol.Application
{
    /// <summary>
    /// Esta clase servira de ayudar para dar una respuesta formatada al cliente.
    /// </summary>
    public class Response
    {
        public bool ThereIsErrors => Errors.Any();

        /// <summary>
        /// Este atributo manejara el Id de algun modelo que se vaya enviar.
        /// </summary>
        public int ModelId { get; set; }

        /// <summary>
        /// Este atributo manejara si el estado de la respuesta es exitosa o no.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Este atributo manejara los mensajes de respuestas que se le mostrara al cliente.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Este atributo manejara el listado de errores que la respuesta enviara al cliente.
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>(0);
    }

    /// <summary>
    /// Esta clase servira de comodin para poder dar una respuesta dinamica segun el tipo de objeto
    /// que se suministre.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : Response where T : class
    {
        /// <summary>
        /// Atributo que manejara una lista de los datos del objeto suministrado.
        /// </summary>
        public IEnumerable<T> DataList { get; set; }

        /// <summary>
        /// Atributo que manejara un objeto unico del tipo que se suministre. 
        /// </summary>
        public T SingleData { get; set; }
    }
}
