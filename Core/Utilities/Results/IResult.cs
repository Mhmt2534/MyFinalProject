using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results;

//Temel voidler çin başlangıç
public interface IResult
{
    bool Success { get; } //get sadece okunabilir demek, set de yazmak için
    string Message { get; }
}
