function delay(ms)
{
    return new Promise(resolve => setTimeout(resolve, ms));
}
async function insertService(trigger, position, game, title) 
{
    document.querySelector(trigger).click();
    await delay(2000);

    var relatedPopupDiv = document.querySelector('.related-popup');
    var iframe = relatedPopupDiv.querySelector('iframe');
    var iframeDocument = iframe.contentDocument || iframe.contentWindow.document;

    iframeDocument.querySelector('select[name="game"]').value = game;

    var event = new Event('change', { bubbles: true });
    iframeDocument.querySelector('select[name="game"]').dispatchEvent(event);

    iframeDocument.querySelector('input[name="pos"]').value = position;
    iframeDocument.querySelector('input[name="title"]').value = title;
    
    await delay(500);

    iframeDocument.querySelector('input[type="submit"][value="Save"]').click();
    await delay(500);
}

await insertService("#add_id_service_options-3-values_options", "1", 5, "Seething Opal of Torment")