using System;
using System.Collections.Generic;
using System.Xml.Serialization;

public class Root
{
   public LogData log;
}


[XmlType(TypeName = "logdata")]
public class LogData
{
    [XmlElement("launchtime")]
    public DateTime launchTime;
    [XmlElement("runduration")]
    public TimeSpan runDuration;
    [XmlElement("commands")]
    public List<string> commands;
    [XmlElement("errormessage")]
    public List<string> errorMessages;

    public LogData()
    {
    }

    public LogData(DateTime launchTime)
    {
        this.launchTime = launchTime;
        this.commands = new List<string>();
        this.errorMessages = new List<string>();
    }

    public void Export(string filepath)
    {
        XmlSerializer ser = new XmlSerializer(typeof(LogData));

        System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
        settings.Indent = true;
        settings.NewLineHandling = System.Xml.NewLineHandling.Entitize;
        System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(filepath, settings);

        ser.Serialize(writer, this);
        writer.Close();
    }
}