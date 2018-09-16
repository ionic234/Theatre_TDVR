

namespace CMGCO.Unity.Interfaces
{
    interface IObjectValidator<T>
    {
        bool Validate(T objectToValidate);
    }

}