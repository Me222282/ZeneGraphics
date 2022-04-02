function addFocusEvent(target)
{
    target.addEventListener("focusin", function(event)
    {
        target.children[1].style.transform = "scaleY(100%)";
        
    }, true);
    
    target.addEventListener("focusout", function(event)
    {
        target.children[1].style.transform = "scaleY(0%)";
    }, true);
}

function addHoverEvent(target)
{
    target.addEventListener("mouseenter", function(event)
    {
        target.children[1].style.transform = "scaleY(100%)";
        
    }, true);
    
    target.addEventListener("mouseleave", function(event)
    {
        target.children[1].style.transform = "scaleY(0%)";
    }, true);
}

var dropdowns = document.getElementsByClassName("nav-dropdown");

for (var n = 0; n < dropdowns.length; n++)
{
    // not dropdown list
    if (dropdowns[n].children.length == 0) { continue; }
    
    addFocusEvent(dropdowns[n]);
    addHoverEvent(dropdowns[n]);
}