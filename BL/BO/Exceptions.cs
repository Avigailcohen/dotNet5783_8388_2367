using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class BlIdDoNotExistException : Exception//if not exist
    {
        public BlIdDoNotExistException(string mess) : base() { }
        public BlIdDoNotExistException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => base.ToString() + $" does not exist";
    }
    [Serializable]
    public class BlIdAlreadyExistException : Exception//if already exist
    {
        public BlIdAlreadyExistException(string mess) : base() { }
        public BlIdAlreadyExistException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => base.ToString() + $" already exist";
    }
    [Serializable]
    public class BlNullPropertyException : Exception
    {
        public string? message;

        public BlNullPropertyException(string mess) : base() { message = mess; }
        public BlNullPropertyException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => $"{message}";

    }
    [Serializable]
    public class BlWrongCategoryException : Exception
    {
        public string? message;
        public BlWrongCategoryException(string mess) : base() { message = mess; }
        public BlWrongCategoryException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => $"{message}";
    }
    [Serializable]
    public class BlIncorrectDateException : Exception
    {
        public string? message;
        public BlIncorrectDateException(string mess) : base() { message = mess; }
        public BlIncorrectDateException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => $"{message}";
    }
    public class BlInvalidInputException : Exception
    {
        public string? Entity;
        public BlInvalidInputException(string ent) : base() { Entity = ent; }
        public BlInvalidInputException(string Entity, Exception innerException) : base(Entity, innerException) { }
        public override string ToString() => base.ToString() + $" Incorrect";
    }
    public class BlEmptyException: Exception
    {

        public BlEmptyException(string? label, int id)

           : base($"{label ?? ""} with id number {id} does not exist.\n")

        {

        }

        public BlEmptyException(string? label)

          : base($"{label ?? ""}.\n")

        {

        }



        public BlEmptyException(string? label, int id, Exception? innerException)

            : base($"{label} with id number {id} does not exist.\n", innerException)

        {

        }

    }
}