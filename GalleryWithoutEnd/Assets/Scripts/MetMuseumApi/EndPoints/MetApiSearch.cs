using System;
using System.Collections;
using System.Collections.Generic;

namespace MetMuseumApi.Endpoints.Search
{
    // Classes for Met API search endpoint

    public class Root
    {
        public int total { get; set; }
        public List<int> objectIDs { get; set; }
    }
}