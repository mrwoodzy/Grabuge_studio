bl_info = {
    "name": "Rig Params Panel",
    "author": "Guillaume Rossi",
    "version": (1, 0),
    "blender": (2, 80, 0),
    "location": "View3D > Sidebar > Rig Params",
    "description": "Panneau de contrôle pour IK/FK, FreeMove, etc.",
    "category": "Rigging"
}

import bpy

class RigToolsPanel(bpy.types.Panel):
    bl_label = "My Rig Properties"
    bl_idname = "VIEW3D_PT_rig_sync"
    bl_space_type = 'VIEW_3D'
    bl_region_type = 'UI'
    bl_category = 'Rig Params'

    def draw(self, context):
        layout = self.layout
        scene = context.scene

        # Switch section
        layout.prop(scene, "show_switch", icon="TRIA_DOWN" if scene.show_switch else "TRIA_RIGHT", emboss=False)
        if scene.show_switch:
            box = layout.box()
            box.label(text="Arms Ik/Fk")

            row = box.row()
            row.prop(scene, "ikFkLArm", text="Left Ik/Fk", slider=True)
            row.prop(scene, "ikFkRArm", text="Right Ik/Fk", slider=True)

            row = box.row()
            row.operator("object.toggle_arm_left", text="Switch Left")
            row.operator("object.toggle_arm_right", text="Switch Right")

            box.label(text="Legs Ik/Fk")
            row = box.row()
            row.prop(scene, "ikFkLLeg", text="Left Ik/Fk", slider=True)
            row.prop(scene, "ikFkRLeg", text="Right Ik/Fk", slider=True)

            row = box.row()
            row.operator("object.toggle_leg_left", text="Switch Left")
            row.operator("object.toggle_leg_right", text="Switch Right")

            box.label(text="Spine Ik/Fk")
            row = box.row()
            row.prop(scene, "ikFkSpine", text="Ik/Fk", slider=True)

            row = box.row()
            row.operator("object.toggle_spine", text="Switch")

        # Match section
        layout.prop(scene, "show_match", icon="TRIA_DOWN" if scene.show_match else "TRIA_RIGHT", emboss=False)
        if scene.show_match:
            box = layout.box()
            box.label(text="Match")

            match_items = [
                ("Arm R", "arm_r"),
                ("Arm L", "arm_l"),
                ("Leg R", "leg_r"),
                ("Leg L", "leg_l"),
                ("Spine", "spine")
            ]

            for label, idname in match_items:
                row = box.row(align=True)
                row.label(text=label)
                row.operator("rig.match_ik_fk", text="IK vers FK").target = idname + "_ik2fk"
                row.operator("rig.match_fk_ik", text="FK vers IK").target = idname + "_fk2ik"

        # FreeMove section
        layout.prop(scene, "show_freemove", icon="TRIA_DOWN" if scene.show_freemove else "TRIA_RIGHT", emboss=False)
        if scene.show_freemove:
            box = layout.box()
            box.label(text="Arms FreeMove")

            row = box.row()
            row.prop(scene, "freeMoveLArm", text="Left", slider=True)
            row.prop(scene, "freeMoveRArm", text="Right", slider=True)

            row = box.row()
            row.operator("object.toggle_freemove_arm_left", text="Switch Left")
            row.operator("object.toggle_freemove_arm_right", text="Switch Right")

            box.label(text="Legs FreeMove")
            row = box.row()
            row.prop(scene, "freeMoveLLeg", text="Left", slider=True)
            row.prop(scene, "freeMoveRLeg", text="Right", slider=True)

            row = box.row()
            row.operator("object.toggle_freemove_leg_left", text="Switch Left")
            row.operator("object.toggle_freemove_leg_right", text="Switch Right")

            box.label(text="SpineIK FreeMove")
            row = box.row()
            row.prop(scene, "freeMoveSpineIK", text="FreeMove", slider=True)

            row = box.row()
            row.operator("object.toggle_freemove_spineik", text="Switch")

            box.label(text="Head FreeMove")
            row = box.row()
            row.prop(scene, "freeMoveHead", text="FreeMove", slider=True)

            row = box.row()
            row.operator("object.toggle_freemove_head", text="Switch")


# Toggle Operator Factory
def make_toggle_operator(idname, label, prop_name):
    class ToggleOperator(bpy.types.Operator):
        bl_idname = idname
        bl_label = label

        def execute(self, context):
            scene = context.scene
            val = getattr(scene, prop_name)
            setattr(scene, prop_name, 1.0 if val == 0.0 else 0.0)
            scene.frame_set(scene.frame_current + 1)
            scene.frame_set(scene.frame_current - 1)
            scene.update_tag()
            context.view_layer.update()
            return {'FINISHED'}
    return ToggleOperator


# Match Operators
class RIG_OT_MatchIKFK(bpy.types.Operator):
    bl_idname = "rig.match_ik_fk"
    bl_label = "Match IK to FK"
    target: bpy.props.StringProperty()

    def execute(self, context):
        print(f"Matching {self.target}: IK to FK")
        return {'FINISHED'}

class RIG_OT_MatchFKIK(bpy.types.Operator):
    bl_idname = "rig.match_fk_ik"
    bl_label = "Match FK to IK"
    target: bpy.props.StringProperty()

    def execute(self, context):
        print(f"Matching {self.target}: FK to IK")
        return {'FINISHED'}


# Register
classes = [
    RigToolsPanel,
    RIG_OT_MatchIKFK,
    RIG_OT_MatchFKIK,
]

toggle_ops = [
    ("object.toggle_arm_left", "Toggle Arm Left", "ikFkLArm"),
    ("object.toggle_arm_right", "Toggle Arm Right", "ikFkRArm"),
    ("object.toggle_leg_left", "Toggle Leg Left", "ikFkLLeg"),
    ("object.toggle_leg_right", "Toggle Leg Right", "ikFkRLeg"),
    ("object.toggle_spine", "Toggle Switch", "ikFkSpine"),
    ("object.toggle_freemove_arm_left", "Toggle FreeMove Arm Left", "freeMoveLArm"),
    ("object.toggle_freemove_arm_right", "Toggle FreeMove Arm Right", "freeMoveRArm"),
    ("object.toggle_freemove_leg_left", "Toggle FreeMove Leg Left", "freeMoveLLeg"),
    ("object.toggle_freemove_leg_right", "Toggle FreeMove Leg Right", "freeMoveRLeg"),
    ("object.toggle_freemove_head", "Toggle FreeMove Head", "freeMoveHead"),
    ("object.toggle_freemove_spineik", "Toggle FreeMove SpineIk", "freeMoveSpineIK")
]

float_props = [
    "ikFkLArm", "ikFkRArm", "ikFkLLeg", "ikFkRLeg", "ikFkSpine",
    "freeMoveLArm", "freeMoveRArm", "freeMoveLLeg", "freeMoveRLeg",
    "freeMoveHead", "freeMoveSpineIK"
]

def register():
    for cls in classes:
        bpy.utils.register_class(cls)
    for idname, label, prop in toggle_ops:
        op_class = make_toggle_operator(idname, label, prop)
        bpy.utils.register_class(op_class)
    for prop in float_props:
        setattr(bpy.types.Scene, prop, bpy.props.FloatProperty(name=prop, default=0.0, min=0.0, max=1.0))
    bpy.types.Scene.show_switch = bpy.props.BoolProperty(name="Show Switch", default=True)
    bpy.types.Scene.show_match = bpy.props.BoolProperty(name="Show Match", default=True)
    bpy.types.Scene.show_freemove = bpy.props.BoolProperty(name="Show FreeMove", default=True)

def unregister():
    for cls in classes:
        bpy.utils.unregister_class(cls)
    for idname, _, _ in toggle_ops:
        op_class = bpy.types.Operator.bl_rna_get_subclass_py(idname)
        if op_class:
            bpy.utils.unregister_class(op_class)
    for prop in float_props + ["show_switch", "show_match", "show_freemove"]:
        if hasattr(bpy.types.Scene, prop):
            delattr(bpy.types.Scene, prop)

if __name__ == "__main__":
    register()
