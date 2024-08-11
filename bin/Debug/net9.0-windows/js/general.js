function delay(ms)
{
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function insertText(advantages, description, reward)
{
    document.getElementById('cke_27').click();
    document.getElementById('cke_104').click();
    document.getElementById('cke_180').click();

    await delay(1000);

    let textareaAdvantages = document.querySelector('textarea[title="Rich Text Editor, id_short_description"]');
    let textareaDescription = document.querySelector('textarea[title="Rich Text Editor, id_description"]');
    let textareaReward = document.querySelector('textarea[title="Rich Text Editor, id_rewards_description"]');

    textareaAdvantages.value = advantages;
    textareaDescription.value = description;
    textareaReward.value = reward;

    document.getElementById('cke_27').click();
    document.getElementById('cke_104').click();
    document.getElementById('cke_180').click();
}

javascript:(function()
{
    document.getElementById("id_hidden").checked = true;

    document.getElementById("id_codename").value = "{&url&}";

    document.getElementById("id_short_title").value = "{&preview&}";

    document.getElementById("id_title").value = "{&title&}";

    document.getElementById("id_title_seo").value = "{&seoTitle&}";
    document.getElementById("id_description_seo").value = "{&seoDescription&}";
})();

await insertText("{&advantages&}", "{&description&}", "{&reward&}");