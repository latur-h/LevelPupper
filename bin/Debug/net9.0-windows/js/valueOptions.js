function delay(ms)
{
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function Service(serviceID, position, title, isRequired, optionType)
{
    document.getElementById("id_service_options-" + serviceID + "-pos").value = position;
    document.getElementById("id_service_options-" + serviceID + "-title").value = title;
    document.getElementById("id_service_options-" + serviceID + "-required").checked = isRequired;

    document.getElementById("id_service_options-" + serviceID + "-option_type").value = document.getElementById("id_service_options-" + serviceID + "-option_type").options[optionType].value;
    var event = new Event('change', { bubbles: true });
    document.getElementById("id_service_options-" + serviceID + "-option_type").dispatchEvent(event);
}
async function BoostMethod(boost_methodID, position, isDefault, method, title)
{
    document.getElementById("id_boost_methods-" + boost_methodID + "-pos").value = position;    
    document.getElementById("id_boost_methods-" + boost_methodID + "-checked_by_default").checked = isDefault;

    document.getElementById("id_boost_methods-" + boost_methodID + "-method").value = document.getElementById("id_boost_methods-" + boost_methodID + "-method").options[method].value;
    var event = new Event('change', { bubbles: true });
    document.getElementById("id_boost_methods-" + boost_methodID + "-method").dispatchEvent(event);

    document.getElementById("id_boost_methods-" + boost_methodID + "-title").value = title;
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

async function insertService(trigger, position, game, title, isDefault) 
{
    while(!_status)
    {
        await delay(100);
    }

    _status = false;

    document.querySelector("#add_id_service_options-" + trigger + "-values_options").click();
    await delay(2000);

    window._popupRef.document.querySelector('select[name="game"]').value = game;
    var event = new Event('change', { bubbles: true });
    window._popupRef.document.querySelector('select[name="game"]').dispatchEvent(event);

    window._popupRef.document.querySelector('input[name="pos"]').value = position;
    window._popupRef.document.querySelector('input[name="title"]').value = title;
    window._popupRef.document.querySelector('input[name="checked_by_default"]').checked = isDefault;

    await delay(500);

    window._popupRef.document.querySelector('input[type="submit"][value="Save"]').click();    
    await delay(500);
}

let _status = true;

document.getElementById("main").addEventListener("click", () => {
    _status = true;
});

async function Currency()
{
    document.querySelector('#tabs-6 #boost_methods-group fieldset.module .add-row a').click();
    await BoostMethod(0, 1, true, 1, "Self-Play");

    document.querySelector('#tabs-8 #service_options-group fieldset.module .add-row a').click();
    await Service(0, 1, "Difficulty", true, 2);
    await insertService(0, 1, 17, "Softcore", true)
    await insertService(0, 2, 17, "Hardcore", false)

    while(!_status)
    {
        await delay(100);
    }

    document.querySelector('#tabs-8 #service_options-group fieldset.module .add-row a').click();
    await Service(1, 2, "League", true, 2);
    await insertService(1, 1, 17, "Standart", true)

    while(!_status)
    {
        await delay(100);
    }

    document.querySelector('#tabs-8 #service_options-group fieldset.module .add-row a').click();
    await Service(2, 3, "Additional Options", true, 4);
    await insertService(2, 1, 17, "Priority", false)
    await insertService(2, 2, 17, "Appear Offline", true)
}

async function Item()
{
    document.querySelector('#tabs-6 #boost_methods-group fieldset.module .add-row a').click();
    await BoostMethod(0, 1, true, 0, "Piloted");

    document.querySelector('#tabs-8 #service_options-group fieldset.module .add-row a').click();
    await Service(0, 1, "Difficulty", true, 2);
    await insertService(0, 1, 17, "Softcore", true)
    await insertService(0, 2, 17, "Hardcore", false)

    while(!_status)
    {
        await delay(100);
    }

    document.querySelector('#tabs-8 #service_options-group fieldset.module .add-row a').click();
    await Service(1, 2, "League", true, 2);
    await insertService(1, 1, 17, "Standart", true)

    while(!_status)
    {
        await delay(100);
    }

    document.querySelector('#tabs-8 #service_options-group fieldset.module .add-row a').click();
    await Service(2, 3, "Additional Options", true, 4);
    await insertService(2, 1, 17, "Priority", false)
    await insertService(2, 2, 17, "Appear Offline", true)
}
async function CharacterBoost()
{
    document.querySelector('#tabs-6 #boost_methods-group fieldset.module .add-row a').click();
    await BoostMethod(0, 1, true, 0, "Piloted");

    document.querySelector('#tabs-8 #service_options-group fieldset.module .add-row a').click();
    await Service(0, 1, "Difficulty", true, 2);
    await insertService(0, 1, 17, "Softcore", true)
    await insertService(0, 2, 17, "Hardcore", false)

    while(!_status)
    {
        await delay(100);
    }

    document.querySelector('#tabs-8 #service_options-group fieldset.module .add-row a').click();
    await Service(1, 2, "League", true, 2);
    await insertService(1, 1, 17, "Standart", true)

    while(!_status)
    {
        await delay(100);
    }

    document.querySelector('#tabs-8 #service_options-group fieldset.module .add-row a').click();
    await Service(2, 3, "Additional Options", true, 4);
    await insertService(2, 1, 17, "Priority", false)
    await insertService(2, 2, 17, "Appear Offline", true)
}

await CharacterBoost();