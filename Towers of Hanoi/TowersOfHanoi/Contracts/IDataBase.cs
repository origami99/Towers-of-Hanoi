using System.Collections.Generic;
using TowersOfHanoi.Models;

namespace TowersOfHanoi.DataBase
{
    public interface IDataBase
    {
        int DiskCounts { get; set; }
        List<Step> Steps { get; set; }
    }
}