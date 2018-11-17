namespace Suzianna.Web.Configuration
{
    public class Host
    {
        public string Name { get; set; }
        public string PhysicalPath { get; set; }
        public HostRunners Runner { get; set; }
        public int Port { get; set; }
    }
}