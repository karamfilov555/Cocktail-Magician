const toggleContainers = function (containers, clickedTabId) {
    for (container of containers) {
        container.style.display = 'none';
    }
    document.getElementById(clickedTabId).style.display = 'block';
}

const toggleTabs = function (tabs, clickedTab) {
    for (tab of tabs) {
        tab.style.color = 'white';
    }
    clickedTab.style.color = 'gold';
}

document.getElementById('tab-switcher').addEventListener('click', function (e) {
    const clickedTabId = e.target.dataset['tab'];
    const containers = document.getElementsByClassName('tab-container');
    const tabs = document.getElementsByClassName('tab-link');
    toggleContainers(containers, clickedTabId);
    toggleTabs(tabs, e.target);
})


  