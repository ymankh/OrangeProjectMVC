var simplemaps_countrymap_mapdata={
  main_settings: {
   //General settings
    width: "responsive", //'700' or 'responsive'
    background_color: "#FFFFFF",
    background_transparent: "yes",
    border_color: "#ffffff",
    
    //State defaults
    state_description: "State description",
    state_color: "#88A4BC",
    state_hover_color: "#3B729F",
    state_url: "",
    border_size: 1.5,
    all_states_inactive: "no",
    all_states_zoomable: "yes",
    
    //Location defaults
    location_description: "Location description",
    location_url: "",
    location_color: "#FF0067",
    location_opacity: 0.8,
    location_hover_opacity: 1,
    location_size: 25,
    location_type: "square",
    location_image_source: "frog.png",
    location_border_color: "#FFFFFF",
    location_border: 2,
    location_hover_border: 2.5,
    all_locations_inactive: "no",
    all_locations_hidden: "no",
    
    //Label defaults
    label_color: "#ffffff",
    label_hover_color: "#ffffff",
    label_size: 16,
    label_font: "Arial",
    label_display: "auto",
    label_scale: "yes",
    hide_labels: "no",
    hide_eastern_labels: "no",
   
    //Zoom settings
    zoom: "yes",
    manual_zoom: "yes",
    back_image: "no",
    initial_back: "no",
    initial_zoom: "-1",
    initial_zoom_solo: "no",
    region_opacity: 1,
    region_hover_opacity: 0.6,
    zoom_out_incrementally: "yes",
    zoom_percentage: 0.99,
    zoom_time: 0.5,
    
    //Popup settings
    popup_color: "white",
    popup_opacity: 0.9,
    popup_shadow: 1,
    popup_corners: 5,
    popup_font: "12px/1.5 Verdana, Arial, Helvetica, sans-serif",
    popup_nocss: "no",
    
    //Advanced settings
    div: "map",
    auto_load: "yes",
    url_new_tab: "no",
    images_directory: "default",
    fade_time: 0.1,
    link_text: "View Website",
    popups: "detect",
    state_image_url: "",
    state_image_position: "",
    location_image_url: ""
  },
  state_specific: {
    JOAJ: {
      name: "عجلون",
      color: "#9f3b73",
      description: "عدد الدائر في عجلون 1 وعدد المقاعد 4",
      hover_color: "#9f913b"
    },
    JOAM: {
      name: "عمان",
      description: "عدد الدوائر 3 وعدد المقاعد 20",
      color: "#3b729f",
      hover_color: "#9f913b"
    },
    JOAQ: {
      name: "العقبة",
      description: "عدد الدوائر 1 وعدد المقاعد 3",
      color: "#3b729f",
      hover_color: "#9f913b"
    },
    JOAT: {
      name: "الطفيلة",
      description: "عدد الدوائر 1 وعدد المقاعد 4",
      color: "#3b729f",
      hover_color: "#9f913b"
    },
    JOAZ: {
      name: "الزرقاء",
      description: "عدد الدوائر 1 وعدد المقاعد 10",
      color: "#3b729f",
      hover_color: "#9f913b"
    },
    JOBA: {
      name: "البلقاء",
      description: "عدد الدوائر 1 وعدد المقاعد 8",
      color: "#3b729f",
      hover_color: "#9f913b"
    },
    JOIR: {
      name: "اربد",
      color: "#6f2950",
      description: "عدد الدوائر في اربد 2 وعدد المقاعد 15",
      hover_color: "#9f913b"
    },
    JOJA: {
      name: "جرش",
      color: "#c5478d",
      description: "عدد الدوائر في جرش 1وعدد المقاعد 4",
      hover_color: "#9f913b"
    },
    JOKA: {
      name: "الكرك",
      description: "عدد الدوائر 1 وعدد المقاعد 8",
      color: "#3b729f",
      hover_color: "#9f913b"
    },
    JOMA: {
      name: "المفرق",
      description: "عدد الدوائر 1 وعدد المقاعد 4",
      color: "#3b729f",
      hover_color: "#9f913b"
    },
    JOMD: {
      name: "مأدبا",
      description: "عدد الدوائر1 وعدد المقاعد 4",
      color: "#3b729f",
      hover_color: "#9f913b"
    },
    JOMN: {
      name: "معان",
      description: "عدد الدوائر 1 وعدد المقاعد 4",
      color: "#3b729f",
      hover_color: "#9f913b"
    }
  },
  locations: {
    "0": {
      name: "عمان",
      lat: "31.95",
      lng: "35.933333"
    }
  },
  labels: {
    "0": {
      name: "عمان",
      x: 279.5,
      y: 359.1,
      parent_type: "location",
      parent_id: "0"
    },
    JOAJ: {
      name: "عجلون",
      parent_id: "JOAJ",
      hover_color: "#010e05"
    },
    JOAM: {
      name: "عمان",
      parent_id: "JOAM",
      hover_color: "#010e05"
    },
    JOAQ: {
      name: "العقبة",
      parent_id: "JOAQ",
      hover_color: "#010e05"
    },
    JOAT: {
      name: "الطفيلة",
      parent_id: "JOAT",
      hover_color: "#010e05"
    },
    JOAZ: {
      name: "الزرقاء",
      parent_id: "JOAZ",
      hover_color: "#010e05"
    },
    JOBA: {
      name: "البلقاء",
      parent_id: "JOBA",
      hover_color: "#010e05"
    },
    JOIR: {
      name: "اربد",
      parent_id: "JOIR",
      hover_color: "#010e05"
    },
    JOJA: {
      name: "جرش",
      parent_id: "JOJA",
      hover_color: "#010e05"
    },
    JOKA: {
      name: "الكرك",
      parent_id: "JOKA",
      hover_color: "#010e05"
    },
    JOMA: {
      name: "المفرق",
      parent_id: "JOMA",
      hover_color: "#010e05"
    },
    JOMD: {
      name: "مأدبا",
      parent_id: "JOMD",
      hover_color: "#010e05"
    },
    JOMN: {
      name: "معان",
      parent_id: "JOMN",
      hover_color: "#010e05"
    }
  },
  legend: {
    entries: []
  },
  regions: {}
};