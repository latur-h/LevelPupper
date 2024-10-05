using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelPupper__Parser.dlls
{
    public class BoostMethod
    {
        public int? id { get; set; }
        public string? title { get; set; }
        public int? method { get; set; }
        public int? price_type { get; set; }
        public object? price_amount { get; set; }
        public object? price_percent { get; set; }
        public object? additional_etc_hours { get; set; }
        public bool? rng_based { get; set; }
        public bool? checked_by_default { get; set; }
    }

    public class Category
    {
        public int? id { get; set; }
        public string? title_seo { get; set; }
        public string? title_h1 { get; set; }
        public string? title { get; set; }
        public string? codename { get; set; }
        public string? image { get; set; }
        public bool? has_children { get; set; }
        public List<object>? children { get; set; }
        public string? short_description { get; set; }
        public string? seo_text { get; set; }
        public string? page_description_block { get; set; }
        public object? one_service_codename { get; set; }
        public string? seo_robots { get; set; }
    }

    public class DescriptionElement
    {
        public int? id { get; set; }
        public string? type { get; set; }
        public bool? show_title { get; set; }
        public bool? with_card { get; set; }
        public string? title { get; set; }
        public string? content { get; set; }
        public List<RelatedService>? related_services { get; set; }
        public List<Element>? elements { get; set; }
    }

    public class DisableValues
    {
        public List<object>? values { get; set; }
        public List<object>? valuesDetailed { get; set; }
    }

    public class Element
    {
        public int? id { get; set; }
        public string? title { get; set; }
        public string? content { get; set; }
        public string? image { get; set; }
    }

    public class Game
    {
        public int? id { get; set; }
        public string? title { get; set; }
        public string? codename { get; set; }
        public string? image { get; set; }
        public string? image_mob { get; set; }
        public string? image_vacancy { get; set; }
        public string? banner_for_custom_order { get; set; }
        public string? icon { get; set; }
        public List<object>? platforms { get; set; }
        public string? color { get; set; }
        public string? seo_robots { get; set; }
    }

    public class LinkElement
    {
        public string? id { get; set; }
        public string? title { get; set; }
    }

    public class Option
    {
        public int? id { get; set; }
        public bool? hidden { get; set; }
        public string? title { get; set; }
        public bool? required { get; set; }
        public string? option_type { get; set; }
        public string? field_placeholder { get; set; }
        public string? field_hint { get; set; }
        public List<RangeGradation>? range_gradations { get; set; }
        public int? range_from_default { get; set; }
        public int? range_from { get; set; }
        public string? range_from_hint { get; set; }
        public int? range_to { get; set; }
        public int? range_to_default { get; set; }
        public string? range_to_hint { get; set; }
        public string? range_style { get; set; }
        public string? range_type { get; set; }
        public List<ValuesOption>? values_options { get; set; }
        public List<object>? time_blocks { get; set; }
        public double? rate_to_amount { get; set; }
        public List<object>? rate_to_servers { get; set; }
        public int? min_amount { get; set; }
        public int? max_amount { get; set; }
        public string? hint_amount { get; set; }
        public bool? show_price { get; set; }
        public bool? show_gradations_title { get; set; }
        public List<object>? show_if_values_selected { get; set; }
        public List<object>? show_if_methods_selected { get; set; }
        public List<object>? show_if_regions_selected { get; set; }
    }

    public class Platform
    {
        public int? id { get; set; }
        public string? title { get; set; }
        public string? icon { get; set; }
    }

    public class RangeGradation
    {
        public int? id { get; set; }
        public string? title { get; set; }
        public string? title_display { get; set; }
        public string? title_color { get; set; }
        public bool? show_as_possible { get; set; }
        public int? number_from { get; set; }
        public int? number_to { get; set; }
        public double? step { get; set; }
        public double? price { get; set; }
        public double? additional_etc_minutes { get; set; }
    }

    public class RelatedService
    {
        public int? id { get; set; }
        public bool? hidden { get; set; }
        public bool? access_by_direct_link { get; set; }
        public string? availability { get; set; }
        public string? codename { get; set; }
        public string? short_title { get; set; }
        public string? title { get; set; }
        public string? short_description { get; set; }
        public string? preview_image { get; set; }
        public string? image { get; set; }
        public string? banner_image { get; set; }
        public string? card_display_type { get; set; }
        public double? price_from { get; set; }
        public double? price_from_display { get; set; }
        public string? price_prefix { get; set; }
        public string? price_sufix { get; set; }
        public double? preview_discount_price { get; set; }
        public Game? game { get; set; }
        public List<object>? tags { get; set; }
        public object? etc_hours { get; set; }
        public bool? rng_based { get; set; }
        public string? execution_option { get; set; }
        public string? seo_robots { get; set; }
    }

    public class Product
    {
        public Service? service { get; set; }
        public List<Option>? options { get; set; }
        public object? service_order { get; set; }
        public List<Category>? categories { get; set; }
    }

    public class Schedules
    {
    }

    public class Service
    {
        public int? id { get; set; }
        public bool? hidden { get; set; }
        public bool? access_by_direct_link { get; set; }
        public string? availability { get; set; }
        public string? codename { get; set; }
        public string? title_seo { get; set; }
        public string? description_seo { get; set; }
        public string? short_title { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? rewards_description { get; set; }
        public string? preview_image { get; set; }
        public string? image { get; set; }
        public double? price_from { get; set; }
        public double? price_from_display { get; set; }
        public string? price_prefix { get; set; }
        public string? price_sufix { get; set; }
        public double? preview_discount_price { get; set; }
        public Game? game { get; set; }
        public List<object>? description_blocks { get; set; }
        public List<DescriptionElement>? description_elements { get; set; }
        public int? completed_count { get; set; }
        public List<Category>? categories { get; set; }
        public object? etc_hours { get; set; }
        public bool? rng_based { get; set; }
        public List<Platform>? platforms { get; set; }
        public List<BoostMethod>? boost_methods { get; set; }
        public List<object>? regions { get; set; }
        public bool? platform_required { get; set; }
        public int? platform_by_default { get; set; }
        public bool? boost_method_required { get; set; }
        public bool? region_required { get; set; }
        public bool? enable_quantity_selection { get; set; }
        public string? quantity_selection_title { get; set; }
        public int? min_quantity { get; set; }
        public int? max_quantity { get; set; }
        public bool? show_quantity_as_range { get; set; }
        public List<object>? quantity_exclude_options { get; set; }
        public List<object>? discount_by_quantity { get; set; }
        public List<LinkElement>? link_elements { get; set; }
        public string? execution_option { get; set; }
        public string? reviews_io_sku { get; set; }
        public string? seo_robots { get; set; }
        public Schedules? schedules { get; set; }
        public bool? manual_booking { get; set; }
    }

    public class ValuesOption
    {
        public int? id { get; set; }
        public string? title { get; set; }
        public int? price_type { get; set; }
        public object? price_amount { get; set; }
        public object? preview_price_amount { get; set; }
        public double? price_percent { get; set; }
        public bool? checked_by_default { get; set; }
        public bool? use_as_basic_price { get; set; }
        public object? additional_etc_hours { get; set; }
        public bool? rng_based { get; set; }
        public DisableValues? disable_values { get; set; }
    }
}
