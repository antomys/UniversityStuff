namespace Lab5EllipticCurves.EllipticService.Interfaces;

public interface IEllipticCurve
{
    bool IsOnCurve(EllipticCurvePoint point);
    EllipticCurvePoint BasePoint { get; set; }
}