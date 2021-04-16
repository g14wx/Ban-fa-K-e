using System;

namespace lab2.Application
{
    
[Flags]
public enum Status 
{
    CREATED = 1,
    DELETED = 0,
    EDITED = 2,
    IDLE = 3
}

}