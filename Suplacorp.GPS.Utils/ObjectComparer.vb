
Public Class ObjectComparer
    Implements IEqualityComparer(Of Object)

    Public Function Equals1(
         ByVal x As Object,
         ByVal y As Object
         ) As Boolean Implements IEqualityComparer(Of Object).Equals

        If x Is y Then Return True

        If x Is Nothing OrElse y Is Nothing Then Return False

        Return (x.Id = y.Id) AndAlso (x.Value = y.Value)
    End Function

    Public Function GetHashCode1(
        ByVal obj As Object
        ) As Integer Implements IEqualityComparer(Of Object).GetHashCode

        If obj Is Nothing Then Return 0

        Dim hashObjValue =
            If(obj.Value Is Nothing, 0, obj.Value.GetHashCode())

        Dim hashObjtId = obj.Id.GetHashCode()

        Return hashObjValue Xor hashObjtId
    End Function
End Class
