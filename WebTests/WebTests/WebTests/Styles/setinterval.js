addLoadEvent(inittimers)

function inittimers() {
    var c = Array("rever", "indi", "stopi", "pausei", "perc", "res1", "start1", "stop1", "res2", "start2", "stop2", "menutop", "for_z1", "for_z2", "for_z3", "for_z4")
    initvar(c)
    d.rever.timer = setInterval(showrever, 1000)
    // showrever()
    d.indi.timer = setInterval(indi, 30)
    d.stopi.onclick = stopi
    d.pausei.onclick = pausei
    d.start1.onclick = showtime1
    d.stop1.onclick = stoptimeout
    d.start2.onclick = startinterval
    d.stop2.onclick = stopinterval
    document.onkeyup = stoptimers
    var el, zlist = d.menutop.rows[0].cells
    for (var i = 0; i < zlist.length; i++) {
        el = zlist[i]
        el.onmouseover = slide
        el.onmouseout = slide
    }
}

function slide(e) {
    e = e || window.event
    var obj = e.relatedTarget || e.fromElement
    // alert(obj.tagName + e.type)
    var ts = findChild(this, "TABLE", "rollsrc"), time = 224, maxh = 40
    if (!ts) return
    ts.style.left = getTopLeft(this).left + 20
    var maxtop = getTopLeft(this).top + this.offsetHeight
    var mintop = getTopLeft(this).top - ts.offsetHeight + this.offsetHeight
    // d.res1.value="maxtop: " + maxtop + "; mintop: " + mintop
    var h = parseInt(ts.offsetHeight), offset = 1, delay = 3
    if (!h) h = 1
    if (h > maxh)
        offset = -Math.floor(-(h / maxh))
    delay = Math.round(time / h) * delay
    if (delay > 20) { delay = 20; offset = 2 }
    d.res1.value = "delay: " + delay + " ms; offset: " + offset + " px"
    // return
    var img = findChild(this, "DIV")
    img = img && findChild(img, "IMG")
    if ("mouseout" == e.type) {
        if (ts.timer1) {
            clearInterval(ts.timer1)
            ts.timer1 = null
        }
        ts.timer2 = setInterval(function () { slide_undo(ts, mintop, offset, img) }, delay)
    }
    else {
        if (img) img.src = "tri2.gif"
        if (ts.timer2) {
            clearInterval(ts.timer2)
            ts.timer2 = null
        }
        if (mintop > getTopLeft(ts).top) ts.style.top = mintop
        ts.timer1 = setInterval(function () { slide_do(ts, maxtop, offset) }, delay)
    }
}

function slide_undo(obj, mintop, offset, img) {
    if (getTopLeft(obj).top > mintop) {
        obj.style.top = getTopLeft(obj).top - offset
    }
    else {
        if (obj && obj.timer2) {
            if (img) img.src = "tri1.gif"
            obj.style.top = getTopLeft(obj).top - 1000
            clearInterval(obj.timer2)
            obj.timer2 = null
        }
    }
}

function slide_do(obj, maxtop, offset) {
    if (getTopLeft(obj).top < maxtop) {
        obj.style.top = getTopLeft(obj).top + offset
    }
    else {
        if (obj && obj.timer1) {
            clearInterval(obj.timer1)
            obj.timer1 = null
        }
    }
}

function stoptimers(e) {
    e = e || window.event
    var el
    if (27 == e.keyCode) {
        for (var i in d) {
            el = d[i]
            //   alert(el.id)
            if (el && el.timer) { clearInterval(el.timer); el.timer = null }
        }
    }
}

function showrever() {
    var d = new Date()
    var offset = d.getTimezoneOffset() * 60 * 1000
    var t = d.getTime()
    d = d.toDateString()
    d = new Date(d)
    d = d.getTime() + 24 * 60 * 60 * 1000
    d = new Date(d - t + offset)
    window.d.rever.value = d.toLocaleTimeString()
}

function indi() {
    var offw = parseInt(d.indi.offsetWidth)
    var perc = Math.round(offw * 100 / 250) + "%"
    d.indi.style.width = offw + 1
    if (perc != d.perc.innerHTML)
        d.perc.innerHTML = perc
    if (offw > 250) {
        clearInterval(d.indi.timer)
    }
}

function stopi() {
    if (this.value == unescape("%u25a0")) {
        clearInterval(d.indi.timer)
        d.indi.timer = null
        d.indi.style.width = 2
        d.perc.innerHTML = "0%"
        addclass(d.pausei, "hidd")
        this.value = unescape("%u25ba")
    }
    else {
        d.indi.timer = setInterval(indi, 30)
        this.value = unescape("%u25a0")
        delclass(d.pausei, "hidd")
    }
}

function pausei() {
    if (d.indi.timer) {
        clearInterval(d.indi.timer)
        d.indi.timer = null
    }
    else {
        d.indi.timer = setInterval(indi, 30)
    }
}

function stoptimeout() {
    if (d.res1.timer) clearTimeout(d.res1.timer)
}

function showtime1() {
    d.res1.value = (new Date()).toLocaleTimeString()
    d.res1.timer = setTimeout(showtime1, 1100)
}

function stopinterval() {
    if (d.res2.timer) clearInterval(d.res2.timer)
}

function startinterval() {
    d.res2.value = (new Date()).toLocaleTimeString()
    d.res2.timer = setInterval(showtime2, 900)
}

function showtime2() {
    d.res2.value = (new Date()).toLocaleTimeString()
}

