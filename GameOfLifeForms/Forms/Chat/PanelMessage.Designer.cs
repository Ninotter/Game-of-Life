namespace GameOfLifeForms.Forms.Chat
{
    partial class PanelMessage
    {
        private System.Windows.Forms.Label labelMessage;

        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            //this: PanelMessage
            this.Size = new System.Drawing.Size(300, 50);
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //labelMessage
            this.labelMessage = new System.Windows.Forms.Label();
            this.labelMessage.Location = new System.Drawing.Point(10, 10);
            this.labelMessage.Size = new System.Drawing.Size(280, 30);
            this.labelMessage.Text = "Message";
            this.labelMessage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Controls.Add(this.labelMessage);
        }

        #endregion
    }
}
