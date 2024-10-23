function delay(ms)
{
    return new Promise(resolve => setTimeout(resolve, ms));
}
async function insertRange(trigger, title, from, to, step, price) 
{
    var id = "#add_id_service_options-" + trigger + "-range_gradations";

    document.querySelector(id).click();
    await delay(2000);

    var relatedPopupDiv = document.querySelector('.related-popup');
    var iframe = relatedPopupDiv.querySelector('iframe');
    var iframeDocument = iframe.contentDocument || iframe.contentWindow.document;

    iframeDocument.querySelector('input[name="title"]').value = title;
    iframeDocument.querySelector('input[name="number_from"]').value = from;
    iframeDocument.querySelector('input[name="number_to"]').value = to;
    iframeDocument.querySelector('input[name="step"]').value = step;
    iframeDocument.querySelector('input[name="price"]').value = price;
    
    await delay(500);

    iframeDocument.querySelector('input[type="submit"][value="Save"]').click();
    await delay(500);
}

await insertRange("0", "Sosi jopu", "1", "5", "2", "10");