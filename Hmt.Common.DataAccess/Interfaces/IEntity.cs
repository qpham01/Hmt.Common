﻿namespace Hmt.Common.DataAccess.Interfaces;

public interface IEntity<T>
{
    T Id { get; set; }
}
