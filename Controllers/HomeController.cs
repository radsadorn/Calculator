using System.Text;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using calculator.Models;

namespace calculator.Controllers;

public class HomeController : Controller
{
    static CalculatorModel calculator = new CalculatorModel{
        valueStrInMemory = "",
        operatorInMemory = "",
        newInput = true
    };
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Button0([FromQuery(Name = "value")] string value)
    {
        string msg = handleNumberClick(value, "0");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult Button1([FromQuery(Name = "value")] string value)
    {
        string msg = handleNumberClick(value, "1");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult Button2([FromQuery(Name = "value")] string value)
    {
        string msg = handleNumberClick(value, "2");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult Button3([FromQuery(Name = "value")] string value)
    {
        string msg = handleNumberClick(value, "3");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult Button4([FromQuery(Name = "value")] string value)
    {
        string msg = handleNumberClick(value, "4");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult Button5([FromQuery(Name = "value")] string value)
    {
        string msg = handleNumberClick(value, "5");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult Button6([FromQuery(Name = "value")] string value)
    {
        string msg = handleNumberClick(value, "6");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult Button7([FromQuery(Name = "value")] string value)
    {
        string msg = handleNumberClick(value, "7");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult Button8([FromQuery(Name = "value")] string value)
    {
        string msg = handleNumberClick(value, "8");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult Button9([FromQuery(Name = "value")] string value)
    {
        string msg = handleNumberClick(value, "9");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult ButtonAC([FromQuery(Name = "value")] string value)
    {
        calculator.valueStrInMemory = "";
        calculator.operatorInMemory = "";
        calculator.newInput = true;
        string msg = "0";
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult ButtonPM([FromQuery(Name = "value")] string value)
    {
        var currentValueNum = getValueAsNum(value);
        var currentValueStr = getValueAsStr(value);

        if (currentValueStr == "0")
        {
            return Content("0", "text/html", Encoding.UTF8); 
        }

        if (currentValueNum >= 0) {
            return Content("-"+value, "text/html", Encoding.UTF8);
        } 
        
        return Content(value.Split('-')[1], "text/html", Encoding.UTF8);
    }

    public IActionResult ButtonPercent([FromQuery(Name = "value")] string value)
    {
        var currentValueNum = getValueAsNum(value);
        var newValueNum = currentValueNum / 100;

        return Content(newValueNum.ToString(), "text/html", Encoding.UTF8);
    }

    public IActionResult ButtonAdd([FromQuery(Name = "value")] string value)
    {
        string msg = handleOperatorClick(value, "addition");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult ButtonSub([FromQuery(Name = "value")] string value)
    {
        string msg = handleOperatorClick(value, "subtraction");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult ButtonMul([FromQuery(Name = "value")] string value)
    {
        string msg = handleOperatorClick(value, "multiplication");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult ButtonDiv([FromQuery(Name = "value")] string value)
    {
        string msg = handleOperatorClick(value, "division");
        return Content(msg, "text/html", Encoding.UTF8);
    }

    public IActionResult ButtonDec([FromQuery(Name = "value")] string value)
    {
        var currentValueStr = getValueAsStr(value);

        if (calculator.newInput) {
            calculator.newInput = false;
            return Content("0.", "text/html", Encoding.UTF8);
        }
        if (!currentValueStr.Contains('.')) {
            currentValueStr += '.';
        }
        return Content(currentValueStr, "text/html", Encoding.UTF8);
    }

    public IActionResult ButtonEqual([FromQuery(Name = "value")] string value)
    {
        var result = "0";
        if (calculator.valueStrInMemory != "")
        {
            result = getResultOfOperationAsStr(value);
            // Console.WriteLine(result);
            calculator.valueStrInMemory = "";
            calculator.operatorInMemory = "";
            calculator.newInput = true;
        }
        return Content(result, "text/html", Encoding.UTF8);
    }

    public string handleOperatorClick(string value, string operation)
    {
        var currentValueStr = getValueAsStr(value);
        // Console.WriteLine(calculator.valueStrInMemory);
        if (calculator.valueStrInMemory == "") {
            calculator.valueStrInMemory = currentValueStr;
            calculator.operatorInMemory = operation;
            calculator.newInput = true;
            // Console.WriteLine("1 "+calculator.valueStrInMemory+" " + calculator.operatorInMemory);
            return calculator.valueStrInMemory;
        }

        if (!calculator.newInput) {
            calculator.valueStrInMemory = getResultOfOperationAsStr(value);
            calculator.operatorInMemory = operation;
            calculator.newInput = true;
        }
        // Console.WriteLine("2 "+calculator.valueStrInMemory+" " + calculator.operatorInMemory);
        return calculator.valueStrInMemory;
    }

    public string getResultOfOperationAsStr(string value)
    {
        var currentValueNum = getValueAsNum(value);
        var valueNumInMemory = Convert.ToDouble(calculator.valueStrInMemory);
        var newValueNum = 0.0;
        if (calculator.operatorInMemory == "addition") {
            newValueNum = valueNumInMemory + currentValueNum;
        } else if (calculator.operatorInMemory == "subtraction") {
            newValueNum = valueNumInMemory - currentValueNum;
        } else if (calculator.operatorInMemory == "multiplication") {
            newValueNum = valueNumInMemory * currentValueNum;
        } else if (calculator.operatorInMemory == "division") {
            newValueNum = valueNumInMemory / currentValueNum;
        }

        return newValueNum.ToString();
    }

    public string handleNumberClick(string value, string numStr)
    {
        var currentValueStr = getValueAsStr(value);
        if (calculator.newInput) {
            calculator.newInput = false;
            return numStr;
        }
        return currentValueStr + numStr;

    }

    private string getValueAsStr(string value) {
        return String.Join("", value.Split(','));
    }

    private double getValueAsNum(string value) {
        return Convert.ToDouble(getValueAsStr(value));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
