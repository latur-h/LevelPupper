function delay(ms)
{
    return new Promise(resolve => setTimeout(resolve, ms));
}

(function interceptWindowOpen()
{
    const originalOpen = window.open;
    window.open = function(...args) 
    {    
        const newWin = originalOpen.apply(this, args);
        window._popupRef = newWin;
        return newWin;
    };
})();

async function insertRange(trigger, title, from, to, step, price) 
{
    while(!_status)
    {
        await delay(100);
    }

    var id = "#add_id_service_options-" + trigger + "-range_gradations";

    document.querySelector(id).click();
    await delay(2000);

    window._popupRef.document.querySelector('input[name="title"]').value = title;
    window._popupRef.document.querySelector('input[name="number_from"]').value = from;
    window._popupRef.document.querySelector('input[name="number_to"]').value = to;
    window._popupRef.document.querySelector('input[name="step"]').value = step;
    window._popupRef.document.querySelector('input[name="price"]').value = price;
    
    await delay(500);

    window._popupRef.document.querySelector('input[type="submit"][value="Save"]').click();
    await delay(500);

    _status = false;
}

let _status = true;

document.getElementById("main").addEventListener("click", () => {
    _status = true;
});

await insertRange("0", "Sosi jopu", "1", "5", "2", "10");