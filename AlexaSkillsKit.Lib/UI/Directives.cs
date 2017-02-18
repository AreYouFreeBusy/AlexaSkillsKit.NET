//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

public class Directive
{
    public virtual string PlayBehavior { get; set; } //TODO: Enum?
    public virtual AudioItem AudioItem { get; set; }
    public string Type { get; set; } //TODO: Enum?
}
